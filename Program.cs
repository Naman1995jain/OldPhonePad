using System;
using OldPhonePadChallenge;

Console.WriteLine("=== Old Phone Keypad Converter Demo ===\n");

// Test cases from the challenge
RunTest("33#", "E");
RunTest("227*#", "B");
RunTest("4433555 555666#", "HELLO");
RunTest("8 88777444666*664#", "TURING");

Console.WriteLine("\n=== Additional Test Cases ===\n");

// Additional examples
RunTest("222 2 22#", "CAB");
RunTest("44 444 0 9966 6#", "HI YOU");
RunTest("222666 6334 4446#", "CODING");
RunTest("1#", "&");
RunTest("0#", " ");
RunTest("2222#", "A");  // Cycling test

Console.WriteLine("\n=== Edge Cases ===\n");

RunTest("#", "");  // Empty
RunTest("*#", "");  // Backspace on empty
RunTest("222***#", "");  // Multiple backspaces

Console.WriteLine("\n=== Error Handling ===\n");

// Test error cases
TestErrorCase(null, "Null input");
TestErrorCase("", "Empty input");
TestErrorCase("222", "Missing send button");

Console.WriteLine("\n=== Interactive Mode ===\n");
Console.WriteLine("Enter your own input (or press Enter to exit):");

while (true)
{
    Console.Write("Input: ");
    string? input = Console.ReadLine();
    
    if (string.IsNullOrWhiteSpace(input))
        break;

    try
    {
        string result = OldPhonePad.ConvertInput(input);
        Console.WriteLine($"Output: {result}\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}\n");
    }
}

Console.WriteLine("\nThank you for using Old Phone Keypad Converter!");

// Local helper functions
void RunTest(string input, string expected)
{
    try
    {
        string result = OldPhonePad.ConvertInput(input);
        bool passed = result == expected;
        string status = passed ? "✓ PASS" : "✗ FAIL";
        
        Console.WriteLine($"{status} | Input: \"{input}\"");
        Console.WriteLine($"       | Expected: \"{expected}\"");
        Console.WriteLine($"       | Got:      \"{result}\"");
        
        if (!passed)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"       | MISMATCH!");
            Console.ResetColor();
        }
        
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"✗ ERROR | Input: \"{input}\"");
        Console.WriteLine($"        | Exception: {ex.Message}");
        Console.ResetColor();
        Console.WriteLine();
    }
}

void TestErrorCase(string? input, string description)
{
    try
    {
        string result = OldPhonePad.ConvertInput(input!);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"✗ FAIL | {description}");
        Console.WriteLine($"       | Should have thrown exception but got: \"{result}\"");
        Console.ResetColor();
    }
    catch (ArgumentException ex)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"✓ PASS | {description}");
        Console.WriteLine($"       | Correctly threw: {ex.GetType().Name}");
        Console.ResetColor();
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"⚠ WARN | {description}");
        Console.WriteLine($"       | Unexpected exception: {ex.GetType().Name}");
        Console.ResetColor();
    }
    Console.WriteLine();
}
