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
- **Terminal system** powered by an interpreter class to process player commands.  
- **Circuit puzzle system** built on a shared interface for electrical components and signal modifiers, with a **state machine** managing transitions.  
- **Dynamic self-assembling system** allows puzzles to be extended or modified at runtime.  
- Iterative **playtesting feedback** informed refinements in gameplay flow and user experience.  

### Example: Terminal Interpreter System

The terminal system allows players to type commands into in-game consoles.  
It is built around an **abstract base class** that enforces a consistent structure, while derived classes implement puzzle-specific logic.  

```csharp
// BaseInterpreter.cs
// Abstract base class for all terminal interpreters.
// Enforces a contract: every interpreter must provide a response list
// and implement how to interpret player input.
public abstract class BaseInterpreter : MonoBehaviour
{
    public abstract List<string> response { get; set; }
    public abstract List<string> Interpert(string input);

    // Example utility: checks if a filename has a valid extension
    protected bool IsValidFile(string fileName)
    {
        if (string.IsNullOrEmpty(fileName)) return false;
        var ext = Path.GetExtension(fileName)?.TrimStart('.').ToLower();
        return new[] { "exe", "cpp", "cs", "py" }.Contains(ext);
    }
}

```
A concrete implementation extends this base, registering commands with handlers.
This makes adding new terminals or puzzles straightforward and modular.

```csharp
// RecyclingInterpreter.cs
// Example concrete terminal. Implements a "Recycling" puzzle terminal.
public class RecyclingInterpreter : BaseInterpreter
{
    private List<string> _response = new List<string>();
    public override List<string> response
    {
        get => _response;
        set => _response = value;
    }

    // Maps commands (strings typed by the player) to their handlers
    private Dictionary<string, Action<string[]>> commandHandlers;

    void Start()
    {
        // Initialize available commands for this terminal
        commandHandlers = new Dictionary<string, Action<string[]>>
        {
            { "help", HandleHelp },
            { "run", HandleRun },
            { "storage", HandleList }
        };
    }

    // Core interpreter: decides what to do with player input
    public override List<string> Interpert(string input)
    {
        response.Clear();
        var args = input.ToLower().Split();

        if (args.Length > 0 && commandHandlers.TryGetValue(args[0], out var handler))
            handler(args);
        else
            response.Add("Command not recognized.");

        return response;
    }

    // Command Handlers -------------------------
    private void HandleHelp(string[] args)
    {
        response.Add("Available commands: help, run, storage");
    }

    private void HandleRun(string[] args)
    {
        if (args.Length < 2) response.Add("Please specify a program.");
        else response.Add($"Running {args[1]}...");
    }

    private void HandleList(string[] args)
    {
        response.Add("Programs: system_ctrl.exe, room_manual.pdf");
        response.Add("Files: log_day_28.txt, log_day_58.txt");
    }
}

```

✅ This modular setup ensures that all terminals share a unified structure, while allowing each puzzle to add its own behavior through specialized command sets.

### Example: Puzzle Composite System

Puzzles are structured using the **Composite pattern**, so multiple components can work together as one larger challenge.

```csharp
// IPuzzleComponent.cs
// Contract for all puzzle elements
public interface IPuzzleComponent
{
    bool CheckCompletion();
    void ResetPuzzle();
}
```

```csharp
// PuzzleComposite.cs
// Composite: a group of puzzle components behaves like one puzzle
public class PuzzleComposite : MonoBehaviour, IPuzzleComponent
{
    private readonly List<IPuzzleComponent> _components = new();

    void Awake()
    {
        foreach (var c in GetComponentsInChildren<IPuzzleComponent>())
            _components.Add(c);
    }

    public bool CheckCompletion() => _components.TrueForAll(c => c.CheckCompletion());
    public void ResetPuzzle() => _components.ForEach(c => c.ResetPuzzle());
}
```
A concrete component, like a FuseBox, plugs into the composite easily:

```csharp
// FuseBox.cs
// Example puzzle piece inside a composite puzzle
public class FuseBox : MonoBehaviour, IPuzzleComponent
{
    public int signal;
    public int requiredSignal;

    public bool CheckCompletion() => signal >= requiredSignal;
    public void ResetPuzzle() => signal = 0;

    void Update()
    {
        if (CheckCompletion())
            Debug.Log("FuseBox solved!");
    }
}
```
✅ This architecture allows small reusable components to scale into complex puzzles while keeping each piece isolated and testable.

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
