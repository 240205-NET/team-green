using Toasted.Logic;
using System.Text;
using System.Globalization;

namespace Toasted.App
{
    public class SearchWeather
    {
        private Location location;
        CurrentWeather currentWeather;
        Weather weather;


        // Constructor
        public SearchWeather(Weather weather, CurrentWeather currentWeather, Location location)
        {
            this.weather = weather;
            this.currentWeather = currentWeather;
            this.location = location;
        }
        
        public void DisplayCurrentWeather(){


            Console.WriteLine("\x1b[31m╔═══════════════════════════════════════════════════════════╗\x1b[0m");
            Console.WriteLine($"                \x1b[31mCurrent Weather for {location.name}\x1b[0m           ");                
            Console.WriteLine("\x1b[31m╚═══════════════════════════════════════════════════════════╝\x1b[0m\n");

            Console.WriteLine($"\x1b[38;5;223m{UnixTimeStampToDateTime(currentWeather.Dt)}\n\x1b[0m");

            DisplayIcon();

            Console.WriteLine("\x1b[38;5;117m┌───────────────────────────────────────────────────────────┐\x1b[0m");
            Console.WriteLine("\x1b[38;5;117m│ \x1b[1m \x1b[34mTemperature\t\x1b[0m \x1b[1m \x1b[34mFeels Like\t\x1b[0m \x1b[1m \x1b[34mSunrise\t\x1b[0m  \x1b[1m \x1b[34mSunset\x1b[0m \x1b[1m  │\x1b[0m");
            Console.WriteLine("\x1b[38;5;117m├───────────────────────────────────────────────────────────┤\x1b[0m");

            
            // Temperature (12°C · 54°F) 
            double tempC = FahrenheitToCelsius(currentWeather.Temp);				
            double feelsLikeC = FahrenheitToCelsius(currentWeather.FeelsLike);		

            Console.WriteLine($"\x1b[22m│ \x1b[36m{tempC}°C·{currentWeather.Temp}°F\t\x1b[0m \x1b[36m{feelsLikeC}°C·{currentWeather.FeelsLike}°F\t\x1b[0m \x1b[36m{UnixTimeStampToTime(currentWeather.Sunrise)}\t\x1b[0m \x1b[36m{UnixTimeStampToTime(currentWeather.Sunset)}\x1b[0m   │\x1b[0m");

            Console.WriteLine("\x1b[38;5;117m└───────────────────────────────────────────────────────────┘\x1b[0m");

        }

        public void DisplayForecast(ForecastApiResponse forecastApiResponse)
        {
          List<StringBuilder> forecastStringBuilderList = GenerateForecastStringBuilderList(forecastApiResponse, 9); // 9 ForecastItem  objects for a full 24-hour forecast
          Console.WriteLine($"\n\x1b[38;5;211mShowing 24-hour forecast for {forecastApiResponse.city}, {forecastApiResponse.country}\x1b[0m\n");
          for (int i = 0; i < forecastStringBuilderList.Count; i++)
          {
            Console.WriteLine(forecastStringBuilderList[i]);
          };
        }
        

        /// <summary>
        /// This method is used to generate a List of StringBuilder objects containing the formatted output of ForecastItem objects that can then be iterated over and displayed neatly in the terminal.
        /// </summary>
        /// <param name="forecastApiResponse">A ForecastApiResponse object.</param>
        /// <param name="numItems">The number of ForecastItem objects to return from the ForecastApiResponse.</param> 
        /// <returns>A list of StringBuilder items each containing output for a single ForecastItem.</returns>
        public static List<StringBuilder> GenerateForecastStringBuilderList(ForecastApiResponse forecastApiResponse, int numItems)
        {
          List<StringBuilder> sbList = new List<StringBuilder>();
          foreach (ForecastItem i in forecastApiResponse.forecastList.forecastItems.Take(numItems).ToList())
          {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\x1b[38;5;22m╔═══════════════════════════════════════╗\x1b[0m");
            string dateAndTime = ConvertUnixTimeToDateTime(i.dt, forecastApiResponse.timezoneOffset);
            sb.AppendLine($" \x1b[38;5;121m {dateAndTime}\x1b[0m\n");
            // Get the icon from the current forecast item
            Icon icon = GetCurrentIcon(i.weather.main);
            // Append each line of the icon to the StringBuilder
            foreach (string line in icon.text)
            {
              sb.AppendLine("\t"+line);
            }
            sb.AppendLine();
            // Description (Moderate Rain, Heavy Rain, etc.)
            sb.AppendLine("\t"+TitleCase(i.weather.Description));		
            // Temperature (12°C · 54°F)
            double tempC = FahrenheitToCelsius(i.main.temp);						

            sb.AppendLine("\t" + FormatTemperatureInColor(tempC, i.main.temp));
            sb.AppendLine("\x1b[38;5;22m╚════════════════════════════════════════╝\x1b[0m");
            sbList.Add(sb);
          }
          return sbList;
        }


        // #################
        // ### Utilities ###
        // #################

        /// <summary>
        /// Converts a string to its title case (e.g., "hello world" becomes "Hello World").
        //  </summary>
        /// <param name="str">The target string.</param>
        /// <returns></returns>
        public static string TitleCase(string str)
        {
          TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
          return textInfo.ToTitleCase(str);
        }

        public static Icon GetCurrentIcon(string category)
        {
          Icon icon = Icons.list.Find(i => i.name == category);
          return icon;
        }

        public static string GetCurrentCountry(string countryCode)
        {
          // Using reflection to get the country name from the description of the country code in the Countries enum
          string countryName = CountryCode.GetEnumDescription((Countries)Enum.Parse(typeof(Countries), countryCode));
          return countryName;
        }

        public static double FahrenheitToCelsius(double fahrenheit)
        {
          double celsius = Math.Truncate((fahrenheit - 32) * 5 / 9);
          return celsius;
        }

        public static string FormatTemperatureInColor(double celsius, double fahrenheit)
        {
          string color;
          var colorMap = new Dictionary<Func<double, bool>, string>
          {
            { temp => temp <= -20, "\u001b[34m" }, // Blue for Extreme Cold
            { temp => temp <= -10, "\u001b[36m" }, // Cyan for Very Cold
            { temp => temp < 0, "\u001b[46m" }, // Dark Cyan for Cold
            { temp => temp < 10, "\u001b[32m" }, // Green for Cool
            { temp => temp < 20, "\u001b[34m" }, // Dark Green for Mild
            { temp => temp < 30, "\u001b[33m" }, // Yellow for Warm
            { temp => temp < 40, "\u001b[33;1m" }, // Dark Yellow (Bright) for Hot
            { temp => temp < 50, "\u001b[31m" }, // Red for Very Hot
            { temp => true, "\u001b[31;1m" } // Bright Red for Extreme Heat
          };
          foreach (var entry in colorMap)
          {
            if (entry.Key(celsius))
            {
              color = entry.Value;
              return $"{color}{celsius}°C · {fahrenheit}°F \u001b[0m";
            }
          }
          return "\u001b[0m"; // this is the default color (stripped)
        }

        public static string ConvertUnixTimeToDateTime(long unixTime, int timezoneOffsetInSeconds)
        {
          DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTime); // The date and time with the UTC offset - looks like: 2021-01-01 18:00:00 +05:00
          TimeSpan offset = TimeSpan.FromSeconds(timezoneOffsetInSeconds); // this is the actual offset relative to UTC - it would look like "-01:30:00" if you gave it -5400 or "01:00:00" if you gave it 3600.
          DateTimeOffset dateTimeWithOffset = dateTimeOffset.ToOffset(offset); // applies the offset to the date and time

          // string formattedDateTime = dateTimeWithOffset.ToString("yyyy-MM-dd HH:mm:ss tt", CultureInfo.InvariantCulture);
          string formattedDateTime = dateTimeWithOffset.ToString("dddd, MMMM dd, yyyy - h:mm tt", CultureInfo.InvariantCulture);



          return formattedDateTime;
        }
        private TimeSpan UnixTimeStampToTime(long unixTimeStamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).LocalDateTime.TimeOfDay;
        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).LocalDateTime;
        }

        public void DisplayIcon(){

            if (weather.Icon == "01d" || weather.Icon == "01n"){
                Console.WriteLine(@"  

                        ======                       
                    ==============                   
                 +==================                 
                ======================               
               ========================              
              =========================              
              ==========================             
              ==========================             
              ==========================             
              =========================+             
               ========================              
               =======================               
                 ====================                
                   ================                  
                      ==========                     
                                                    ");
            }

            if(weather.Icon == "02d" || weather.Icon == "02n")
            {
                Console.WriteLine(@"
                                     
                                                     
                                ======               
                            =============            
                   ........+===============          
                 ...........================         
                .............+++++==========         
                ..................:+========         
             .......................+=======         
           .........................+++=====         
          .............................:+==          
         .................................           
         .................................           
          ...............................            
            ............................             
                                                     
                                   
                ");
            }
    
            if(weather.Icon== "03d" || weather.Icon == "03n"){
                Console.WriteLine(@"
                                                               
                     ......                          
                   ...........                       
                  .............                      
                 ...................                 
                .....................                
             .........................               
           ..............................            
           ................................          
           ................................          
            ...............................          
             ............................            
                                                     
                ");
            }

            if(weather.Icon == "04d" || weather.Icon == "04n"){
                Console.WriteLine(@"
                                                           
                            ####                     
                         #########                   
                  ......=%#########                  
                ..........-%############             
               ............:%############            
               .............=   .###########         
            ......................=##########        
          ........................-%#########        
        ..............................*######        
        ...............................:%##          
        ................................             
         ...............................             
           ...........................               
      
                ");
            }

            if(weather.Icon == "09d" || weather.Icon == "09n"){
                Console.WriteLine(@"
                              
                          XXXXXX                     
                      .  XXXXXXXX                    
                  .......:XXXXXXXXXXX                
                 ..........XX$XXXXXXXX               
                 ..............:$XXXXXXXX            
             ...................:$XXXXXXXX           
            ......................:XXXXXXX           
           .........................:$XX             
           ..........................                
             .......:................                
               ....X$ ...... .....                   
                  X   XX   XX                        
                 XXX  XX                             
                  X  XX  XXX                         
                     XX  XX                          
                         
                ");
            }

            if(weather.Icon == "10d" || weather.Icon == "10n"){
                Console.WriteLine(@"
                                                     
                             ++++++++                
                      ...  ++++++++++++              
                   ........;++++++++++++             
                  ..........;++++++++++++            
                  ...............++++++++            
              ....................+++++++            
             .......................;++              
             .........................               
             .........................               
              .......;................               
                 ...XX......  .....                  
                   $   XXX  XX                       
                  XXX  XX                            
                      XX  XXX                        
                      XX  XX                         
                                                            
                ");
            }

            if(weather.Icon == "11d" || weather.Icon == "11n"){
                Console.WriteLine(@"
                                              
                         XXXXXXXXX                   
                   .....$XXXXXXXXXX                  
                ..........XXXXXXXXXXXXX              
               ............XXXXXXXXXXXXX             
               ............::...xXXXXXXXXXX          
            .....................;$XXXXXXXXX         
          .......................:&$XXXXXXXX         
         ............................+$XXXXX         
         ..............................              
         ..........++++................              
          ........;++;................               
            .....;+++++..............                
                 +++++                               
                  ++                                 
                 ++                                                                          
                ");
            }

            if(weather.Icon == "13d" || weather.Icon == "13n"){
                Console.WriteLine(@"
                    
                      XX XXX XX                      
                   XXXXXXXXXXXXXXX                   
                 XXXXX  XXXXX  XXXXX                 
                 XXXXXX  XXX  XXXXXX                 
                XXXXXXXXXXXXXXXXXXXXX                
                 XX    XXX XXX     X                 
                XXXXXXXXXXXXXXXXXXXXX                
                  XXXXX  XXX  XXXXX                  
                 XXXXXX  XXX   XXXXXX                
                   XXX XXXXXXX XXX                   
                      XXXXXXXXX                      
                         XXX                                                                                                  
                ");
            
            }

             if(weather.Icon == "50d" || weather.Icon == "50n"){
                Console.WriteLine(@"
                                                
                      XXXXXXXXXX                         
                      XXXXXXXXXX                         
               XXXXXXXXXXXXXXXXXXX                       
                                                         
                     XXXXXXXXXXXXXXXXXXXXXXX             
                      $XXXXXXXXXXXXXXXXXXXX              
             XXXXXXXXXXXXXXXXXXXXXXX                     
                                                         
                 XXXXXXXXXXXXXXXXXXXXXXXX                
                                                         
                     XXXXXXXXXXXXXXX                     
                    
                ");
            
            }
        }

    }
}