# StringUtils

A zero-allocation string manipulation library for C#.

Designed for performance-critical environments where avoiding heap allocations and GC pressure matters — such as game development, real-time systems, or hot code paths.

---

## The Problem

In C#, strings are immutable. Every concatenation or modification allocates a new string on the heap:

```csharp
// Each + allocates a new string
string result = "Hello" + " " + name + "!";
```

In performance-sensitive scenarios (e.g. Unity game loop, high-frequency logging, network packet formatting) this causes GC pressure and frame spikes.

---

## How It Works

`StringUtils` bypasses string immutability by pinning the string in memory and writing characters directly via `Marshal.WriteInt16`. This means modifications happen **in-place** with zero heap allocations.

```csharp
// Writes directly into the existing string's memory — no allocation
str.ReplaceAt(index, newChar);
```

> **Note:** This technique is intentionally unsafe by C# conventions. It is designed for controlled, performance-critical scenarios where you own the string and understand the implications.

---

## Components

### StringExtension

Extension methods for in-place string mutation:

```csharp
// Replace the first occurrence of a character
str.ReplaceChar('a', 'b');

// Replace all occurrences of a character
str.ReplaceAll('a', 'b');

// Replace a character at a specific index
str.ReplaceAt(3, 'X');
```

### StringCreator

A reusable string builder that operates on a fixed-size pre-allocated buffer. Supports appending strings, characters, integers, and booleans without allocating on each operation.

```csharp
var creator = new StringCreator(64); // allocate once

creator.Append("Score: ");
creator.Append(42);
creator.Append(" pts");

string result = creator; // implicit cast, no extra allocation
creator.Clear();         // reset and reuse
```

Operator overloads for ergonomic usage:

```csharp
creator + "Hello" + ' ' + 99 + true;
```

Supports index access and mutation:

```csharp
char c = creator[0];
creator[0] = 'X';
```

---

## Installation

Copy the `StringUtils` folder into your project, or install via UPM *(coming soon)*.

---

## Use Cases

- Unity UI text updates in the game loop
- High-frequency logging without GC pressure
- Network packet or protocol string formatting
- Any hot path where `StringBuilder` allocations are unacceptable

---

## License

MIT
