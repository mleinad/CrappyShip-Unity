# Crappy Ship

A Unity-based **first-person puzzle/escape room** experience developed as part of the Applied Project for the final year of a bachelor’s degree.  
This project represents the most complex and longest-running game development I’ve worked on, spanning several months in collaboration with two colleagues.

---

## 🚀 Project Overview

**Crappy Ship** is set in the year **2050**, during the **first human voyage to Mars**. The spacecraft, operated by an AI system, challenges players to explore different rooms and solve intricate puzzles to ensure the mission’s success.  

Gameplay combines immersive exploration with logic-driven problem solving, where each puzzle is designed to test both observation and reasoning skills.

---

## 🧑‍🤝‍🧑 Team

- **Daniel** — Game Design & Programming  
- **Gabriel** — Game Design & Art  
- **Rafael** — Game Design & Programming  

---

## 🎮 Gameplay & Features

- **First-person puzzle/escape room mechanics** in a sci-fi environment.  
- A variety of rooms to explore, each with distinct challenges:  
  - Engineering Room  
  - Navigation Room  
  - Electrical Room  
  - Reactor Room  
  - Gyroscope Room  
- **Interactive terminals** that act as in-game computers, featuring ASCII-style displays and player-driven command input.  
- **Circuit-building puzzles** where players snap components together, simulating logic-gate-like behavior.  
- Modular project structure designed to support scalability and future upgrades.  

---

## 🖥️ Development Insights

This project emphasized **system architecture and modular design** to support complex gameplay:  

- **Composite design pattern** used to structure puzzles, ensuring reusability and flexibility.  
- **Singleton-like Event Manager** for global communication across systems.  
- **Terminal system** powered by an interpreter class to process player commands, with a manager handling formatting and presentation. All interpreters inherit from a base class for scalability.  
- **Circuit puzzle system** built on a shared interface for electrical components and signal modifiers, with a **state machine** managing transitions (idle, picked up, active).  
- **Dynamic self-assembling system** allows puzzles to be extended or modified at runtime.  
- Iterative **playtesting feedback** informed refinements in gameplay flow and user experience.  

---

## 🖼️ Visuals & Art

- Coherent sci-fi aesthetic consistent across environments and puzzles.  
- Map and room design evolved alongside puzzle development.  
- Custom art assets integrated to enhance immersion.  

---

## 📊 Development Effort

- **Total development time:** ~303 hours  
- **Focus areas:**  
  - Programming (59%)  
  - Art (21%)  
  - Game Design (13%)  
  - Research & Sound (7%)  

---

## ✅ Strengths

- Complex puzzle mechanics with modular underpinnings  
- Interactive and engaging terminal systems  
- Expandable architecture supporting new puzzles and features  
- Clear and coherent visual direction  
- Positive results from playtesting and feedback cycles  

---

## ⚠️ Limitations

- Requires further polish for performance optimization  
- Contains minor bugs affecting stability  
- Narrative implementation remains incomplete  

---

## 🔗 Resources

[Gameplay Preview](https://docs.google.com/file/d/1IwjYsG6_8i65EWFYw7OYzgd6WIsGSUWL/preview)
