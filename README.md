# Unity Tools

A lightweight, robust, and decoupled **ScriptableObject-Driven Architecture** framework for Unity. Designed to eliminate direct component dependencies, promote clean data flow, and simplify gameplay state management.

Built under the `JuanBorges.Unity.Tools` namespace with full unit test coverage using NUnit EditMode tests.

---

## 📄 Overview

`Unity Tools` provides a clean implementation of ScriptableObject-driven patterns—popularized by Ryan Hipple—to separate data storage, event handling, and logic into modular, testable components.

### Key Pillars
- **Variables & References (`VariableSO<T>` / `VariableReference<T>`):** Flexible data wrappers allowing designers to easily toggle between hardcoded inspector constants and shared ScriptableObject state (`UseConstant = true/false`).
- **Event Channels:** Decoupled event broadcasting systems to reduce direct component coupling.
- **Design Patterns:** Creational and architectural utilities built natively for Unity's lifecycle.
- **Unit Tested:** Built for production stability with full EditMode test coverage for core variable types and mathematical operations.

---

## 🛠️ Architecture & Project Structure

```text
Toolkit/
├── JuanBorges.Unity.Tools.asmdef
├── Design Patterns/
│   └── Creational/
├── Event Channels/
├── Variables/
│   ├── Core/
│   ├── Editor/
│   └── Tests/
│       └── JuanBorges.Unity.Tools.Tests.asmdef
```

### Namespace Hierarchy

To keep autocompletion clean and maintain a clear separation of concerns, the framework uses structured sub-namespaces:

```csharp
using JuanBorges.Unity.Tools.Variables;
using JuanBorges.Unity.Tools.Events;
using JuanBorges.Unity.Tools.DesignPatterns;
```

---

## 🚀 Key Features

### 1. Seamless Variable References (`VariableReference<T>`)
Every reference type (`FloatReference`, `IntReference`, `Vector3Reference`, etc.) automatically routes read/write operations through its `.Value` property depending on `UseConstant`:

```csharp
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector3Reference moveDirection;
    [SerializeField] private FloatReference moveSpeed;

    private void Update()
    {
        // Math operators delegate directly to .Value seamlessly
        transform.position += moveDirection * (moveSpeed * Time.deltaTime);
    }
}
```

### 2. Built-in Math & Operator Overloads
Types like `Vector2Reference` and `Vector3Reference` overload standard vector operations (`+`, `-`, `*`, `/`, unary `-`) so you can perform natural mathematical calculations without needing `.Value` syntax every time.

```csharp
Vector3 positionOffset = playerPosition + velocityReference;
Vector3 scaledDirection = directionReference * 2.5f;
Vector3 invertedVector = -vectorReference;
```

### 3. Change Detection & Observer Events
Subscribe directly to data changes across scene boundaries:

```csharp
[SerializeField] private IntVariableSO playerScore;

private void OnEnable() => playerScore.OnValueChanged += UpdateUI;
private void OnDisable() => playerScore.OnValueChanged -= UpdateUI;

private void UpdateUI(int newScore)
{
    scoreText.text = $"Score: {newScore}";
}
```

---

## 🧪 Unit Tests

This package includes comprehensive EditMode tests built with NUnit to verify math overloads, implicit type conversions, null safety, and event broadcasting.

To run the test suite:
1. Open Unity.
2. Navigate to **Window > General > Test Runner**.
3. Select **EditMode**.
4. Click **Run All** to run `JuanBorges.Unity.Tools.Tests`.

---

## 📦 Supported Types

- **Primitives:** `Bool`, `Int`, `Float`, `String`
- **Vectors:** `Vector2`, `Vector3`
- **Custom Structures:** `FloatRange` (`Min`/`Max`)

---

## 👤 Author

**Juan Borges**  
- GitHub: [@JuanBorges](https://github.com/JuanBorges)