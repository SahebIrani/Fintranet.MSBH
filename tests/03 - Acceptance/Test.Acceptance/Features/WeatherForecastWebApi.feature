Feature: Weather Forecast Web API

Hi everybody, Go2sh .. !!!! ♥️💖 
Minimal Web API for weather forecasts ..

@MSBH
Scenario: Get weather forecasts
  Given I'm a client
  And the repository has weather data
  When I make a GET request to 'weatherforecast'
  Then the response status code is '200'
  And the response json should be 'List<WeatherForecast>'
