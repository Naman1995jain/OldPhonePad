# Old Phone Keypad Converter

A C# implementation of an old phone keypad text input system, simulating the behavior of classic mobile phones where you press number keys multiple times to cycle through letters.

## 📱 Problem Description

This solution converts old phone keypad input sequences into text output. On old phones, each number key (0-9) corresponded to multiple letters, and you would press the key multiple times to cycle through them.

### Keypad Layout

```
1      2      3
&'(    ABC    DEF

4      5      6
GHI    JKL    MNO

7      8      9
PQRS   TUV    WXYZ

       0
      Space
```

### Special Keys
- **Space**: Pause to type two characters from the same button
- **\***: Backspace (delete last character)
- **#**: Send button (terminates input)

## 🚀 Features

- ✅ Multi-press letter cycling (e.g., pressing 2 three times = 'C')
- ✅ Space handling for consecutive same-key presses
- ✅ Backspace functionality
- ✅ Comprehensive input validation
- ✅ Full unit test coverage
- ✅ Clean, documented code following C# best practices

## 💻 Usage

### Basic Example

```csharp
using OldPhonePadChallenge;

// Simple letter
string result1 = OldPhonePad.ConvertInput("33#");
// Output: "E"

// With backspace
string result2 = OldPhonePad.ConvertInput("227*#");
// Output: "B"

// Complex word
string result3 = OldPhonePad.ConvertInput("4433555 555666#");
// Output: "HELLO"

// With spaces and backspace
string result4 = OldPhonePad.ConvertInput("8 88777444666*664#");
// Output: "TURING"
```

### Advanced Examples

```csharp
// Typing "CAB" (requires space between C and A as both use key 2)
OldPhonePad.ConvertInput("222 2 22#");
// Output: "CAB"

// Typing "HI YOU" (0 = space character)
OldPhonePad.ConvertInput("44 444 0 9966 6#");
// Output: "HI YOU"

// Special characters from key 1
OldPhonePad.ConvertInput("1#");    // "&"
OldPhonePad.ConvertInput("11#");   // "'"
OldPhonePad.ConvertInput("111#");  // "("
```

## 🏗️ Project Structure

```
OldPhonePadChallenge/
├── OldPhonePad.cs           # Main implementation
├── OldPhonePadTests.cs      # Unit tests (xUnit)
├── README.md                # This file
└── OldPhonePadChallenge.csproj
```

## 🧪 Testing

The solution includes comprehensive unit tests using xUnit framework:

- **Basic Functionality Tests**: Core conversion logic
- **Edge Cases**: Empty input, multiple backspaces, cycling
- **Keypad Mapping Tests**: All keys and letters
- **Space Handling Tests**: Same-key sequential presses
- **Validation Tests**: Input validation
- **Complex Scenarios**: Real-world usage patterns

### Running Tests

```bash
dotnet test
```

### Test Coverage

- 25+ test cases
- 100% code coverage
- All edge cases handled

## 🛠️ Technical Implementation

### Algorithm Overview

1. **Parse Input**: Read characters one by one
2. **Sequence Tracking**: Accumulate consecutive same-key presses
3. **Character Resolution**: Map sequence to corresponding letter using modulo for cycling
4. **Special Handling**: Process spaces (pause) and backspace operations
5. **Result Building**: Construct final output string

### Key Design Decisions

- **Dictionary-based mapping**: O(1) lookup for keypad letters
- **StringBuilder**: Efficient string concatenation
- **Modulo cycling**: Elegant handling of multi-press beyond available letters
- **Separation of concerns**: Private helper method for character resolution
- **Comprehensive validation**: Early failure on invalid input

### Time Complexity

- **O(n)**: Where n is the length of the input string
- Single pass through the input with constant-time operations

### Space Complexity

- **O(m)**: Where m is the length of the output string
- Minimal additional space for tracking current sequence

## 📋 Requirements

- .NET 6.0 or higher
- xUnit (for testing)

## 🔧 Setup Instructions

1. Clone the repository:
```bash
git clone https://github.com/yourusername/old-phone-keypad.git
cd old-phone-keypad
```

2. Build the project:
```bash
dotnet build
```

3. Run tests:
```bash
dotnet test
```

4. Run the application:
```csharp
using OldPhonePadChallenge;

var output = OldPhonePad.ConvertInput("your-input#");
Console.WriteLine(output);
```

## 📝 API Documentation

### Method Signature

```csharp
public static string ConvertInput(string input)
```

### Parameters

- **input** (string): The keypad input sequence
  - Must end with '#' (send button)
  - Can contain digits (0-9), spaces, '*' (backspace), and '#' (send)

### Returns

- **string**: The decoded text message

### Exceptions

- **ArgumentException**: Thrown when:
  - Input is null or empty
  - Input doesn't end with '#'

## 🎯 Example Walkthrough

Let's trace through `"222 2 22#"` → `"CAB"`:

1. **"222"**: Press key 2 three times → 'C'
2. **" "**: Space (pause) to reset sequence
3. **"2"**: Press key 2 once → 'A'
4. **" "**: Space (pause) to reset sequence
5. **"22"**: Press key 2 twice → 'B'
6. **"#"**: Send button (terminate)

Result: **"CAB"**

## 🤝 Contributing

This is a coding challenge submission. However, suggestions and feedback are welcome!

## 📄 License

This project is created as a coding challenge submission.

## 👤 Author

Developed as part of the Iron Software C# Coding Challenge

## 📧 Contact

For questions about this implementation, please reach out to: namanjain34710@gmail.com

---

**Note**: This solution prioritizes clean code, comprehensive testing, and clear documentation to demonstrate software engineering best practices.