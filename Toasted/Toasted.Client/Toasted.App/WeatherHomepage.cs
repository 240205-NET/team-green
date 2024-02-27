using Toasted.Logic;

namespace Taosted.App
{
    public class WeatherHomepage {
    
        CurrentWeather currentWeather;
        Weather weather;
        User user;


        // Constructor
        public WeatherHomepage(Weather weather, CurrentWeather currentWeather, User user)
        {
            this.weather = weather;
            this.currentWeather = currentWeather;
            this.user = user;
        }
        
        public void DisplayCurrentWeather(){




            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║             Current Weather for {user.location.name.ToUpper()}             ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝\n");

            Console.WriteLine($"{UnixTimeStampToDateTime(currentWeather.dt)}\n");

            DisplayIcon();

            Console.WriteLine("\x1b[1m┌──────────────┬──────────────┬──────────────┬──────────────┐\x1b[0m");
            Console.WriteLine("\x1b[1m│ \x1b[34mTemperature\x1b[0m      \x1b[1m│ \x1b[34mFeels Like\x1b[0m       \x1b[1m│ \x1b[34mSunrise\x1b[0m         \x1b[1m│ \x1b[34mSunset\x1b[0m          \x1b[1m│\x1b[0m");
            Console.WriteLine("\x1b[1m├──────────────┼──────────────┼──────────────┼──────────────┤\x1b[0m");

            // Use string formatting to align the columns
            Console.WriteLine($"\x1b[1m│ \x1b[36m{currentWeather.temp,-14:F2}\x1b[0m │ \x1b[36m{currentWeather.feelsLike,-16:F2}\x1b[0m │ \x1b[36m{UnixTimeStampToDateTime(currentWeather.sunrise),-16}\x1b[0m │ \x1b[36m{UnixTimeStampToDateTime(currentWeather.sunset),-16}\x1b[0m │\x1b[0m");

            Console.WriteLine("\x1b[1m└──────────────┴──────────────┴──────────────┴──────────────┘\x1b[0m");

        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).LocalDateTime;
        }

        public void DisplayIcon(){

            if (weather.icon == "01d"){
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

            if(weather.icon == "02d")
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
    
            if(weather.icon== "03d"){
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

            if(weather.icon == "04d" || weather.description == "overcast clouds"){
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

            if(weather.icon == "09d"){
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

            if(weather.icon == "10d"){
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

            if(weather.icon == "11d"){
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

            if(weather.icon == "13d"){
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

             if(weather.icon == "50d"){
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