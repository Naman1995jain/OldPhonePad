using System;
using System.Text;
using System.Collections.Generic;

namespace OldPhonePadChallenge
{
    /// <summary>
    /// Converts old phone keypad input sequences into text output.
    /// Simulates the behavior of old mobile phones where you press number keys multiple times
    /// to cycle through letters (e.g., pressing 2 three times gives 'C').
    /// </summary>
    public class OldPhonePad
    {
        // Keypad mapping: each digit maps to its corresponding letters
        private static readonly Dictionary<char, string> KeypadMap = new Dictionary<char, string>
        {
            { '0', " " },      // Space
            { '1', "&'(" },    // Special characters
            { '2', "ABC" },
            { '3', "DEF" },
            { '4', "GHI" },
            { '5', "JKL" },
            { '6', "MNO" },
            { '7', "PQRS" },
            { '8', "TUV" },
            { '9', "WXYZ" }
        };

        /// <summary>
        /// Converts an old phone keypad input string into the corresponding text output.
        /// </summary>
        /// <param name="input">
        /// The input string containing:
        /// - Digits (0-9): Key presses
        /// - Space: Pause between same-key presses
        /// - *: Backspace
        /// - #: Send/terminate
        /// </param>
        /// <returns>The decoded text message</returns>
        /// <exception cref="ArgumentException">Thrown when input is null, empty, or doesn't end with #</exception>
        public static string ConvertInput(string input)
        {
            // Input validation
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Input cannot be null or empty", nameof(input));

            if (!input.EndsWith("#"))
                throw new ArgumentException("Input must end with '#' (send button)", nameof(input));

            // Remove the send button from processing
            input = input.Substring(0, input.Length - 1);

            var result = new StringBuilder();
            var currentSequence = new StringBuilder();
            char lastKey = '\0';

            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];

                // Handle backspace
                if (currentChar == '*')
                {
                    // Process any pending sequence first
                    if (currentSequence.Length > 0)
                    {
                        AppendCharacterFromSequence(result, currentSequence.ToString(), lastKey);
                        currentSequence.Clear();
                    }
                    
                    // Remove last character from result
                    if (result.Length > 0)
                        result.Length--;
                    
                    lastKey = '\0';
                    continue;
                }

                // Handle space (pause between same-key presses)
                if (currentChar == ' ')
                {
                    // Process the accumulated sequence
                    if (currentSequence.Length > 0)
                    {
                        AppendCharacterFromSequence(result, currentSequence.ToString(), lastKey);
                        currentSequence.Clear();
                    }
                    lastKey = '\0';
                    continue;
                }

                // Handle digit keys
                if (char.IsDigit(currentChar))
                {
                    // If this is a different key, process the previous sequence
                    if (lastKey != '\0' && lastKey != currentChar)
                    {
                        AppendCharacterFromSequence(result, currentSequence.ToString(), lastKey);
                        currentSequence.Clear();
                    }

                    currentSequence.Append(currentChar);
                    lastKey = currentChar;
                }
            }

            // Process any remaining sequence
            if (currentSequence.Length > 0)
            {
                AppendCharacterFromSequence(result, currentSequence.ToString(), lastKey);
            }

            return result.ToString();
        }

        /// <summary>
        /// Appends the character corresponding to a key press sequence.
        /// </summary>
        /// <param name="result">The result string builder</param>
        /// <param name="sequence">The sequence of same key presses (e.g., "222")</param>
        /// <param name="key">The key that was pressed</param>
        private static void AppendCharacterFromSequence(StringBuilder result, string sequence, char key)
        {
            if (!KeypadMap.ContainsKey(key))
                return;

            string letters = KeypadMap[key];
            int pressCount = sequence.Length;

            // Calculate which letter to use (cycling through available letters)
            // Subtract 1 because pressing once gives the first letter (index 0)
            int index = (pressCount - 1) % letters.Length;
            result.Append(letters[index]);
        }
    }
}