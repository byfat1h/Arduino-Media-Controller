//C# Media Controller V2.2 Arduino Code - byFat1h
#define next_button 9
#define prev_button 7
#define play_pause_button 8
#define volume_up_button 12
#define volume_down_button 10
#define mute_unmute_button 11

int next_data = 0;
int prev_data = 0;
int pause_data = 0;
int volumeup_data = 0;
int volumedown_data = 0;
int mute_data = 0;

void setup() {
 Serial.begin(9600);
 for (int i=7;i<13;i++)
 {
   pinMode(i,INPUT);
  }
}

void loop() {
  next_data = digitalRead(next_button);
  prev_data = digitalRead(prev_button);
  pause_data = digitalRead(play_pause_button);
  volumeup_data = digitalRead(volume_up_button);
  volumedown_data = digitalRead(volume_down_button);
  mute_data = digitalRead(mute_unmute_button);

 if(next_data==1)
 {
  Serial.println("B"); 
  delay(250);
  }

 if(prev_data==1)
 {
  Serial.println("C");
  delay(250);
 }

 if(pause_data==1)
 {
  Serial.println("A"); 
  delay(250);
  }
  if(volumeup_data==1)
 {
  Serial.println("D"); 
  delay(300);
  }

 if(volumedown_data==1)
 {
  Serial.println("E");
  delay(300);
 }

 if(mute_data==1)
 {
  Serial.println("F"); 
  delay(250);
  }
}
//byFat1h
