# 🎮 Clicky Crates – Data-Driven Arcade Game



**Play it live on itch.io 👉 [Clicky Crates on itch.io](https://arpan-akm.itch.io/clicky-crates)**  

---

## 📖 About This Project
**Clicky Crates** is a fast-paced, data-driven arcade clicker built in Unity.  
It’s a *supercharged* version of the **Unity Junior Programmer Pathway final prototype**, rebuilt with scalable, modular architecture.

Your goal?  
👉 Click the good crates before they fall.  
👉 Avoid the bad ones.  
👉 Beat the high score and make your name the champion!  

---

## ✨ Features
- 🖱️ **Simple & Addictive Gameplay** – Easy to learn, hard to master.  
- 📈 **Multiple Difficulty Levels** – Easy, Medium, Hard.  
- 🏆 **Persistent High Scores** – Stores champion name & score across sessions.  
- 📊 **Data-Driven Design** – Target types and point values are loaded from `targets.json`.  
- 📝 **Player Analytics Log** – Every session is stored in `playerlog.json` (score, name, duration, difficulty).  

---

## 🛠️ Technical Highlights
- **Singleton Pattern** (`DataManager.cs`) for global data access.  
- **Asynchronous Loading** via Coroutines & `UnityWebRequest`.  
- **JSON Serialization** using Unity’s `JsonUtility`.  
- **Dual Persistence** – `PlayerPrefs` for quick data, JSON logs for analytics.  
- **UI Management** with TextMeshPro.  

---

## 🚀 How to Run

### Play in Browser
Play instantly on itch.io:  
👉 [https://arpan-akm.itch.io/clicky-crates](https://arpan-akm.itch.io/clicky-crates) 
<p>
   <H9>OR <br>
      For Better Expirenence</H9>
</p>

### Run Locally
1. **Prerequisites:**  
   - Unity Hub  
   - Unity Editor `2022.3.x` (LTS or newer)  
   - Git  

2. **Clone this repository:**
   ```bash
   git clone https://github.com/arpan-akm/clicky-crates.git
   cd clicky-crates
---

## 🙌 Credits
Developed by **Arpan Kumar Moharana**  
Inspired by the **Unity Junior Programmer Pathway (Prototype 5)**
