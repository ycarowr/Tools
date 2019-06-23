# Push/Pop Generic Finite State Machine in Unity using Monobehaviors

This is an implementation of an abstract Push/Pop Generic Finite State Machine using the Monobehavior as a base class. In the text below you can find more information about the code as well as pictures of a sample test scene. I hope this code can be useful for someone else.

Some details:
- The concrete state machine and all the states (components) have to be attached to a single gameobject before the initialization. It's better to have all in a single prefab.
- The implementation demands the user/programmer to keep each State in a single separated file/monobehavior/component, in other words, it enforces the single responsibility principle of each state (see more in https://en.wikipedia.org/wiki/Single_responsibility_principle).
- Good flexibility to make Editor shenanigans such as assign variables to states or use of coroutines. I'd like to remind that all the unity callbacks also live in each state because everything is inside the "Monobehavior world", for simple games its amazing, however not all the games can "afford" it; 
- All the core methods are documented with its own summary. You can also find plenty of other comments adding extra information about the code;

Where to use? 

This implementation is very useful to manage a turn-based game, where you can create single states with their own specific behaviors and control the game flow, example:

1. Player Turn State: handle actions during the turn of generic player. Example: Time out of the turn;
2. User Turn State: handle actions of the User. Example: Enable Input;
2. Ai Turn State: handle actions of the AI. Example: contains the Ai module to calculate the best card to be played in a CCG;
3. Pregame Setup State: handle all the pre-game configurations. Example: decide which player goes first and draw starting hand;
4. Game Finished State: Win/Lose (both can be broken in two single separated states as well)

- You will find a sample scene with the usage of the FSM here in the main repo: https://github.com/ycarowr/Tools it's located in the following path: Assets/Scenes/TestStateMachineMB.unity
- The test scene contains a concrete class for the state machine, for the states and some tests;
- The current version has some logs along the method. You might remove it.

This is how looks like the prefab of the test state machine inside the sample scene:

![alt text](https://github.com/ycarowr/Tools/blob/master/Assets/Scripts/Patterns/StateMachineMB/prefab%20struct.GIF)

These are the logs after the state machine initialization:

![alt text](https://github.com/ycarowr/Tools/blob/master/Assets/Scripts/Patterns/StateMachineMB/fsmstart.GIF)

The picture below shows the logs after the push operation of the AiState

![alt text](https://github.com/ycarowr/Tools/blob/master/Assets/Scripts/Patterns/StateMachineMB/aistate.GIF)

The picture below shows the logs after some push and pop operations 

![alt text](https://github.com/ycarowr/Tools/blob/master/Assets/Scripts/Patterns/StateMachineMB/operations.GIF)

