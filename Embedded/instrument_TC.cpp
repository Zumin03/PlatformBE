#include <Arduino.h>
#include <Adafruit_MCP9808.h>

const char *instrumentName = "Thermal Commander";
const char *measurementType = "temperature";
const char *measurementUnit = "°C";
const char *instrumentId = "TC-00000";
const char *softwareVersion = "1.1";

const char *cmdMeasure = "MEASURE";
const char *cmdIdentify = "IDENTIFY";
const char *cmdSelfTest = "SELFTEST";

void handleMeasurement();
void handleIdentification();
void selfTest();

Adafruit_MCP9808 sensor = Adafruit_MCP9808();

void setup()
{

  Serial.begin(9600);
  if (!sensor.begin(0x18))
  {
    Serial.println("Unable to connect to the MCP9808 breakout board!");
    Serial.println("Check your connections and verify the address is correct.");
    while (1)
      ;
  }
  Serial.println("MCP9808 initialized!");

  // Wake up the sensor from its sleep mode
  Serial.println("Waking up MCP9808.... ");
  sensor.wake();

  sensor.setResolution(2);
}

void loop()
{
  if (Serial.available())
  {
    char cmdBuffer[32];
    int len = Serial.readBytesUntil('\n', cmdBuffer, sizeof(cmdBuffer) - 1);
    cmdBuffer[len] = '\0';
    if (strcmp(cmdBuffer, cmdMeasure) == 0)
    {
      handleMeasurement();
    }
    else if (strcmp(cmdBuffer, cmdIdentify) == 0)
    {
      handleIdentification();
    }
    else if (strcmp(cmdBuffer, cmdSelfTest) == 0)
    {
      selfTest();
    }
    delay(100);
  }
}

void handleMeasurement()
{
  float tempValue = sensor.readTempC();
  char valueBuffer[10];
  dtostrf(tempValue, 0, 2, valueBuffer);

  char responseBuffer[64];
  sprintf(responseBuffer, "{\"instrumentId\" : \"%s\", \"value\" : %s}", instrumentId, valueBuffer);
  Serial.println(responseBuffer);
}

void handleIdentification()
{
  char responseBuffer[256];
  sprintf(responseBuffer, "{\"instrumentId\" : \"%s\", \"instrumentName\" : \"%s\", \"channel\" : \"%s\", \"unit\" : \"%s\", \"softwareVersion\" : \"%s\"}", instrumentId, instrumentName, measurementType, measurementUnit, softwareVersion);
  Serial.println(responseBuffer);
}

void selfTest()
{
  if (sensor.begin(0x18))
  {
    sensor.wake();
    char responseBuffer[64];
    float test = sensor.readTempC();
    if (test >= -20.0 && test <= 50.0)
    {
      sprintf(responseBuffer, "{\"instrumentId\" : \"%s\", \"sensorState\" : \"%s\"}", instrumentId, "OK");
      Serial.println(responseBuffer);
    }
    else
    {
      sprintf(responseBuffer, "{\"instrumentId\" : \"%s\", \"sensorState\" : \"%s\"}", instrumentId, "INA");
      Serial.println(responseBuffer);
    }
  }
  else
  {
    char responseBuffer[64];
    sprintf(responseBuffer, "{\"instrumentId\" : \"%s\", \"sensorState\" : \"%s\"}", instrumentId, "ERR");
    Serial.println(responseBuffer);
  }
}