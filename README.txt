Toy Robot Challenge - Readme

Description:
This application lets you place a toy robot on a board, and will allow you to move it around. Additionally, the program will not let the toy robot fall down should you move it over the edge of the board.

Technical Details:
The board is 5x5 units large, drawn as below:
o o o o o 
o o o o o 
o o o o o 
o o o o o 
o o o o o 
The toy robot can be place anywhere, and will have an arrow icon corresponding to where it is facing:
^ - facing North
v - facing South
> - facing East
< - facing West

The following actions can be read:
PLACE X,Y,F
MOVE
LEFT
RIGHT
REPORT

PLACE - will put the toy robot on the table in position X,Y and facing the F direction: NORTH, SOUTH, EAST or WEST. The origin (0,0) is SOUTH WEST most corner of the board.
MOVE - will move the toy robot one unit forward in the direction it is currently facing.
LEFT - will rotate the direction the robot is facing 90 degrees to the left, without changing its position.
RIGHT - will rotate the direction the robot is facing 90 degrees to the right, without changing its position.
REPORT - will announce the X,Y and F direction of the robot.

It is required that the first command to the robot is a PLACE command, after that, any sequence of commands may be issued, in any order, including another PLACE command.
Entering an invalid commands or an invalid PLACE command (i.e. non-numerics, negative numbers and number > 5) will return an Invalid Command result.

Input Options:
1. standard input
	Run the application by opening a terminal in the bin/release/net6.0 folder and typing "Iress_Coding_Challenge.exe "
2. file input
	Create a .txt file and log your commands separated by a new line.  Run the application by opening a terminal in the bin/release/net6.0 folder and typing "Iress_Coding_Challenge.exe <your_filename>.txt"
	
Unit Testing:
	Run the unit test by opening a terminal in the base folder and typing "dotnet test"