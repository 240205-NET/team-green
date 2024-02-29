# team-green

Toasted Project Description

Toasted is a web application leveraging the .NET Framework with ASP.NET API and employing a console application for user interaction. Its core functionality revolves around fetching weather data based on user-specified locations. This includes retrieving both current weather information and a five-day forecast. Additionally, users can maintain a personalized list of locations for monitoring purposes. Integral to its operation is the integration of the OpenWeather API to access current weather data, which is complemented by an in-house API facilitating communication with a proprietary database. Security measures are implemented, including password encryption and database authentication, to ensure data confidentiality.

Under the hood, Toasted employs ASCII art to visually represent weather conditions within the console interface. Leveraging the OpenWeather API, it retrieves essential weather parameters such as locale (city and country), weather conditions (rain, mist, fog, etc.), and temperature readings in both Celsius and Fahrenheit. Persistent storage of user-specific location preferences is managed through an Azure T-SQL database with ADO.NET, ensuring seamless retrieval and management of personalized weather data. A simple web interface allows simplified interaction through the browser.

Stand-ups:

5/22/2023

Lawrance: 
- Created a user class with a constructor 
- No problems

Vishal:
- Broke down test cases for user name and password
- Problems: figuring out how encryption works 
- Will solve the encryption problem
  
Matthew:
- Made functions to query weather API
- Various classes for WeatherAPI responses
- Problem: OpenWeatherAPI apikey
- Will code front-end
  
Meryem: 
- Created tables 
- Connected the database 
- Problems: Double datatype was not accepted and needed to be converted it to float instead
- Will create.DATA 

2/27/2024

Lawrance:
- made location serialized object to pass the api
- user class takes the location serialized
- changed some database variables and types
- checked user functions, created some unit tests
- you can get a user object 

Visshal:
- The register method works with the client side
- Check if the account already exists, error handling, looping
- Password enc, login function
- Problems: await async for menu display, menu keeps displaying

Matthew:
- Implemented get forecast asynchronous method, 
- Added artwork
- Problems: artworks are not horizontal but vertical
- Have the forecast for hourly, daily
  
Meryem:
- Created  WeatherHomepage class including displayCurrentWeather method for the weather homepage
- Formatted lines and values with ASCII color codes
- Created table lines using ASCII box drawing characters
- Created weather condition icons using ASCII art and matched them with icon codes from the API response
- Created methods to convert unix time to DateTime format

2/29/2024

Lawrence:
- Created API methods to handle the patch to change password, location, and temp reference, and created async functions on the client side.
- Verified email, check if the email exists
- Created tests for all async classes
  
Matthew:
- Added hourly icons
- Refactor menu.cs
- Edited request.cs
- Get 24hr forecast
- Get 5-day forecast and display it
- Couldn’t get the menu exit, it’s fixed later
  
Meryem:
- Created display hourly forecast method for weatherHomepage
- Formatted lines with ASCII color codes
- Created table lines using ASCII box drawing characters
  
Visshal:
- Initial menu works
- Register and login is fixed
- UserLogin menu is created. The problem was having a lot of methods and static to non-static to enable to access
- UserLogin, called the patch methods that Lawrence creates
- Called weather homepage with current, hourly, and daily
- Made the menu loop so it doesn’t break


