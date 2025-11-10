# BubbleSurvivr 🎮  

**BubbleSurvivr** is a top-down 2D game where the player must dodge and pop bubbles of different sizes.  
Each bubble has its own speed and deals damage according to its size: bigger bubbles are slower but more harmful, while smaller bubbles are faster but deal less damage.  
Random power-ups and power-downs appear on the field, adding unpredictable twists to the gameplay.  

The game was created for a **48-hour Game Jam** themed around **"bubbles"**.  
Inspired by *Bubble Trouble*, we decided to create a new twist with full field movement instead of restricting the player to the bottom of the screen.  

---

## 🎥 Demo Video  
<iframe src="https://drive.google.com/file/d/1g77jFDMVl8_TTmQ8A_4alHriZxvtj3be/preview" width="640" height="360" allow="autoplay"></iframe>  

---

## 🖼️ Screenshots  

### Main Menu  
![Main Menu](https://github.com/omershukroon/BubbleSurvivrGJ-/blob/main/Media/main_menu.png?raw=true)  

### Stage Selection  
![Levels Screen](https://github.com/omershukroon/BubbleSurvivrGJ-/blob/main/Media/levels_screen.png?raw=true)  

#### Level Types  
- **Locked Level:**  
  ![Locked Level](https://github.com/omershukroon/BubbleSurvivrGJ-/blob/main/Media/lock_level.png?raw=true)  
  Locked levels require a certain number of stars to unlock.  
- **Unlocked Level:**  
  ![Open Level](https://github.com/omershukroon/BubbleSurvivrGJ-/blob/main/Media/open_level.png?raw=true)  
  Levels that are currently available based on the player’s progress.  
- **Coming Soon Level:**  
  ![Coming Soon](https://github.com/omershukroon/BubbleSurvivrGJ-/blob/main/Media/coming-soon_level.png?raw=true)  
  Placeholder buttons for upcoming stages.  
  Each button checks its index against the total number of available stages — if the index is higher, it automatically becomes a “Coming Soon” level.  

---

### End Level Screen  
![End Level](https://github.com/omershukroon/BubbleSurvivrGJ-/blob/main/Media/end_level_screen.png?raw=true)  

At the end of each stage, the player can:  
- Retry the stage using the **“Try Again”** button.  
- Return to the stage selection screen using the **“Exit”** button.  

#### ⭐ Star Rating System  
The player’s score is based on remaining health:  
- **3 Stars:** Above 75 HP  
- **2 Stars:** Between 50–75 HP  
- **1 Star:** Between 25–50 HP  
- **0 Stars:** Below 25 HP  

---

## ⚡ Power-Ups & Power-Downs  

### Blessing  
![Blessing](https://github.com/omershukroon/BubbleSurvivrGJ-/blob/main/Media/blessing_1.png?raw=true)  
**Effect:** Allows the player to shoot continuously without pauses.  

### Curse  
![Curse 1](https://github.com/omershukroon/BubbleSurvivrGJ-/blob/main/Media/curse_1.png?raw=true)  
![Curse 2](https://github.com/omershukroon/BubbleSurvivrGJ-/blob/main/Media/curse_2.png?raw=true)  
**Effect:** Inverts the player’s movement controls — left/right and up/down are swapped.  

### Heal  
![Heal 1](https://github.com/omershukroon/BubbleSurvivrGJ-/blob/main/Media/heal_1.png?raw=true)  
![Heal 2](https://github.com/omershukroon/BubbleSurvivrGJ-/blob/main/Media/heal_2.png?raw=true)  
**Effect:** Gradually restores **20 HP** to the player.  

### Rocks  
![Rocks 1](https://github.com/omershukroon/BubbleSurvivrGJ-/blob/main/Media/rocks_1.png?raw=true)  
![Rocks 2](https://github.com/omershukroon/BubbleSurvivrGJ-/blob/main/Media/rocks_2.png?raw=true)  
**Effect:** Spawns rocks on the field. The player and bubbles **cannot pass through** them — but the player’s **arrows can**.  

### Slow Down  
![Slow Down 1](https://github.com/omershukroon/BubbleSurvivrGJ-/blob/main/Media/slow_down_1.png?raw=true)  
![Slow Down 2](https://github.com/omershukroon/BubbleSurvivrGJ-/blob/main/Media/slow_down_2.png?raw=true)  
**Effect:** Reduces the player’s movement speed by **20%**.  

### Speed Up  
![Speed Up 1](https://github.com/omershukroon/BubbleSurvivrGJ-/blob/main/Media/speed_up_1.png?raw=true)  
![Speed Up 2](https://github.com/omershukroon/BubbleSurvivrGJ-/blob/main/Media/speed_up_2.png?raw=true)  
**Effect:** Increases the player’s movement speed by **20%**.  

---

## 🎮 Controls  
- **Movement:** WASD  
- **Attack:** Mouse Click  

---

## 🧠 What We Learned  
- Experimented with **object pooling** and **Instantiate** in 2D (we were used to 3D from studies).  
- Learned to handle **animations** and **particle systems** in a 2D environment.  
- Biggest lesson: always define the **game idea**, **features**, and **mechanics** clearly from the start — otherwise, adding new ideas along the way can make it hard to finish everything on time.  

---

## 👥 Credits  
- Created by [Omer Shukron](https://github.com/omershukroon) & [Kim Tsadok](https://github.com/KimTsadok)  
- Assets from **Unity Asset Store** & **Kenney.nl**  
- Built for a **48-hour Game Jam** on the theme *"Bubbles"*.  
