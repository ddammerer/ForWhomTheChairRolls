#include <Arduino.h>

// Encoder 1
#define inputCLK1 1
#define inputDT1  2

// Encoder 2
#define inputCLK2 3
#define inputDT2  4

int counter1 = 0;
int counter2 = 0;

int currentStateCLK1;
int previousStateCLK1;

int currentStateCLK2;
int previousStateCLK2;

String encdir1 = "";
String encdir2 = "";

void setup() {
  pinMode(inputCLK1, INPUT_PULLUP);
  pinMode(inputDT1, INPUT_PULLUP);

  pinMode(inputCLK2, INPUT_PULLUP);
  pinMode(inputDT2, INPUT_PULLUP);

  Serial.begin(9600);

  previousStateCLK1 = digitalRead(inputCLK1);
  previousStateCLK2 = digitalRead(inputCLK2);
}

void loop() {

  // ----- Encoder 1 -----
  currentStateCLK1 = digitalRead(inputCLK1);

  if (currentStateCLK1 != previousStateCLK1) {
    if (digitalRead(inputDT1) != currentStateCLK1) {
      counter1--;
      encdir1 = "ccw";
    } else {
      counter1++;
      encdir1 = "cw";
    }

    Serial.print("Enc1 Dir: ");
    Serial.print(encdir1);
    Serial.print(" Rotation: ");
    Serial.println(counter1);
  }

  previousStateCLK1 = currentStateCLK1;


  // ----- Encoder 2 -----
  currentStateCLK2 = digitalRead(inputCLK2);

  if (currentStateCLK2 != previousStateCLK2) {
    if (digitalRead(inputDT2) != currentStateCLK2) {
      counter2--;
      encdir2 = "ccw";
    } else {
      counter2++;
      encdir2 = "cw";
    }

    Serial.print("Enc2 Dir: ");
    Serial.print(encdir2);
    Serial.print(" Rotation: ");
    Serial.println(counter2);
  }

  previousStateCLK2 = currentStateCLK2;
}
