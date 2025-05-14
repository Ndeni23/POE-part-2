CyberSecurity Awareness ChatBot

Overview
This project is a simple chatbot designed to provide cybersecurity awareness by answering common security-related questions. It is implemented in C# and runs as a console application. The chatbot interacts with users, provides responses to predefined questions, and includes features such as ASCII logo rendering and an audio-based welcome message.

Features
- User Interaction: Users can enter their name and ask questions related to cybersecurity.
- Predefined Responses: The chatbot has a list of stored responses to common cybersecurity topics.
- Filtering System: The chatbot ignores common words (e.g., "is", "the", "how") to improve response accuracy.
- Multimedia Support:
  - Displays an ASCII version of a bot logo.
  - Plays a welcome audio file when the chatbot starts.
- Modular Structure:
  - ChatBot (Main class) - Handles user interactions and chatbot flow.
  - AudioImageHandler (Subclass) - Manages audio playback and image rendering.
  - QuestionHandler (Subclass) - Processes user questions and retrieves responses.

File Structure
CyberSecurityBot/
│-- Program.cs          # Entry point of the application
│-- ChatBot.cs         # Main chatbot logic
│-- AudioImageHandler.cs  # Handles audio and image processing
│-- QuestionHandler.cs # Manages user questions and responses
│-- bot.jpg            # Logo image (ASCII conversion)
│-- twin.wav           # Welcome audio file
│-- README.md          # Documentation

How It Works
1. The program starts and loads the chatbot.
2. A loading animation is displayed.
3. The bot logo (converted to ASCII) is displayed.
4. The welcome audio is played.
5. The user is asked for their name.
6. The chatbot prompts the user to ask cybersecurity-related questions.
7. If the question contains relevant keywords, an appropriate predefined response is provided.
8. The user can exit at any time by typing "exit".

Setup & Execution
Prerequisites
- .NET Core installed
- C# development environment (e.g., Visual Studio, VS Code)

Running the Application
1. Clone or download the repository.
2. Open the project in your preferred C# development environment.
3. Ensure bot.jpg and twin.wav are placed in the correct directory.
4. Build and run the project.

Customization
- Modify bot.jpg if you want a different ASCII logo.
- Replace twin.wav with a different welcome audio.
- Add new cybersecurity-related responses in the StoreReplies method within QuestionHandler.cs.

Future Enhancements
- Implement Natural Language Processing (NLP) for better question understanding.
- Connect to an external API for real-time cybersecurity updates.
- Introduce a GUI-based interface.

Author
CyberSecurityBot Team

License
This project is licensed under the MIT License.
