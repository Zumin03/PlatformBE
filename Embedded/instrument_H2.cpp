#include <Arduino_SensorKit.h>

const char *instrumentName = "Humidator 2000";
const char *measurementType = "humidity";
const char *measurementUnit = "%";
const char *instrumentId = "H2-00000";
const char *softwareVersion = "1.0";

const char *cmdMeasure = "MEASURE";
const char *cmdIdentify = "IDENTIFY";
const char *cmdSelfTest = "SELFTEST";

void handleMeasurement();
void handleIdentification();
void selfTest();

#define DHT20 Environment_I2C

void setup()
{
  Serial.begin(9600);
  DHT20.begin();
  delay(300);
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
  float tempValue = DHT20.readHumidity();
  char valueBuffer[10];
  dtostrf(tempValue, 0, 2, valueBuffer);

  char responseBuffer[128];
  sprintf(responseBuffer, "{\"instrumentId\" : \"%s\", \"value\" : %s}", instrumentId, valueBuffer);
  Serial.println(responseBuffer);
}

void handleIdentification()
{
  char responseBuffer[128];
  sprintf(responseBuffer, "{\"instrumentId\" : \"%s\", \"instrumentName\" : \"%s\", \"channel\" : \"%s\", \"unit\" : \"%s\", \"softwareVersion\" : \"%s\"}", instrumentId, instrumentName, measurementType, measurementUnit, softwareVersion);
  Serial.println(responseBuffer);
}

void selfTest()
{
  if (DHT20.begin())
  {
    char responseBuffer[64];
    float test = DHT20.getHumidity();
    if (test >= 0.0 && test <= 100.0)
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