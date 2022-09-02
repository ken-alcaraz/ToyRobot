namespace ToyRobotChallenge
{
    public static class GlobalVars{
        public static List<ToyRobot> trList;
        public static int robotID = 0;
        public const int xLimitPos = 5;
        public const int yLimitPos = 5;
        public const int xLimitNeg = 0;
        public const int yLimitNeg = 0;
        public const string com_PLACE = "PLACE";
        public const string com_MOVE = "MOVE";
        public const string com_LEFT = "LEFT";
        public const string com_RIGHT = "RIGHT";
        public const string com_REPORT = "REPORT";
        public const string dir_NORTH = "NORTH";
        public const string dir_SOUTH = "SOUTH";
        public const string dir_EAST = "EAST";
        public const string dir_WEST = "WEST";
        public const string com_INVALID = "INVALID";
        public const string com_EXIT = "EXIT";

        public static List<ToyRobot> TrList;
    }
    
    public class TRCommand{
        public string command;
        public int xPos;
        public int yPos;
        public string direction;
    }

    public class ToyRobot{
        int id;
        public int xPosition;
        public int yPosition;
        public string direction;

        public ToyRobot (int idin, int xPositionin, int yPositionin, String directionin){
            id = idin;
            xPosition = xPositionin;
            yPosition = yPositionin;
            direction = directionin;
        }
        
    }

    class TROperations{
        public TRCommand validateInput(string input){
            TRCommand result = new TRCommand();
            char[] delimiterChars = {' ',','};
            bool xRes, yRes, validateFlag;
            
            validateFlag = true;
            input = input.Trim();
            string[] placeOp = input.Split(delimiterChars,StringSplitOptions.RemoveEmptyEntries);

            //validate PLACE operation
            if (placeOp.Length == 4 && String.Equals(placeOp[0],GlobalVars.com_PLACE, StringComparison.OrdinalIgnoreCase) == true){
                
                xRes = int.TryParse(placeOp[1], out result.xPos);
                yRes = int.TryParse(placeOp[2], out result.yPos);

                if (xRes == true || xRes == true){
                    if ((result.xPos >= GlobalVars.xLimitNeg && result.xPos < GlobalVars.xLimitPos) && (result.yPos >= GlobalVars.yLimitNeg && result.yPos < GlobalVars.yLimitPos)){
                        if (String.Equals(placeOp[3],GlobalVars.dir_NORTH, StringComparison.OrdinalIgnoreCase) == true || 
                        String.Equals(placeOp[3],GlobalVars.dir_SOUTH, StringComparison.OrdinalIgnoreCase) == true ||
                        String.Equals(placeOp[3],GlobalVars.dir_EAST, StringComparison.OrdinalIgnoreCase) == true ||
                        String.Equals(placeOp[3],GlobalVars.dir_WEST, StringComparison.OrdinalIgnoreCase) == true){
                            result.command = placeOp[0].ToUpper();
                            result.direction = placeOp[3].ToUpper();
                        }
                        else
                            validateFlag = false;
                    }
                    else
                        validateFlag = false;
                }
                else
                    validateFlag = false;
            }
            else if (placeOp.Length == 1){
                //validate other operations
                if (String.Equals(placeOp[0],GlobalVars.com_MOVE, StringComparison.OrdinalIgnoreCase) == true || 
                    String.Equals(placeOp[0],GlobalVars.com_LEFT, StringComparison.OrdinalIgnoreCase) == true ||
                    String.Equals(placeOp[0],GlobalVars.com_RIGHT, StringComparison.OrdinalIgnoreCase) == true ||
                    String.Equals(placeOp[0],GlobalVars.com_REPORT, StringComparison.OrdinalIgnoreCase) == true||
                    String.Equals(placeOp[0],GlobalVars.com_EXIT, StringComparison.OrdinalIgnoreCase) == true)
                    result.command = placeOp[0];
                else
                    validateFlag = false;
            }
            else
                validateFlag = false;

            if (validateFlag == false)
                result.command = GlobalVars.com_INVALID;

            return result;
        }
        public string performCommand(TRCommand input, ToyRobot toyrobot){
            string result = "";
            if (String.Equals(input.command,GlobalVars.com_PLACE, StringComparison.OrdinalIgnoreCase) == true){
                toyrobot = new ToyRobot(GlobalVars.robotID++,input.xPos,input.yPos,input.direction);
                if ( GlobalVars.TrList.Any() == true)
                    GlobalVars.TrList.RemoveAt(GlobalVars.TrList.Count - 1);
                GlobalVars.TrList.Add(toyrobot);
                Console.WriteLine("Robot placed at " + toyrobot.xPosition + ", " + toyrobot.yPosition + ", " + toyrobot.direction);
                result = input.command;
            }
            else if (String.Equals(input.command,GlobalVars.com_MOVE, StringComparison.OrdinalIgnoreCase) == true){
                if (GlobalVars.TrList.Any() == true){
                    switch (GlobalVars.TrList.Last().direction){
                        case (GlobalVars.dir_NORTH):
                            GlobalVars.TrList.Last().yPosition+=1;
                            if (GlobalVars.TrList.Last().yPosition >= GlobalVars.yLimitPos){
                                GlobalVars.TrList.Last().yPosition = GlobalVars.yLimitPos-1;
                                Console.WriteLine("Robot has reached northern limit");
                            }
                            break;
                        case (GlobalVars.dir_WEST):
                            GlobalVars.TrList.Last().xPosition-=1;
                            if (GlobalVars.TrList.Last().xPosition <= GlobalVars.xLimitNeg){
                                GlobalVars.TrList.Last().xPosition = GlobalVars.xLimitNeg;
                                Console.WriteLine("Robot has reached western limit");
                            }
                            break;
                        case (GlobalVars.dir_SOUTH):
                            GlobalVars.TrList.Last().yPosition-=1;
                            if (GlobalVars.TrList.Last().yPosition <= GlobalVars.yLimitNeg){
                                GlobalVars.TrList.Last().yPosition = GlobalVars.yLimitNeg;
                                Console.WriteLine("Robot has reached southern limit");
                            }
                            break;
                        case (GlobalVars.dir_EAST):
                            GlobalVars.TrList.Last().xPosition+=1;
                            if (GlobalVars.TrList.Last().xPosition >= GlobalVars.xLimitPos){
                                GlobalVars.TrList.Last().xPosition = GlobalVars.xLimitPos-1;
                                Console.WriteLine("Robot has reached eastern limit");
                            }
                            break;
                    }
                    result = input.command;
                }
                else{
                    Console.WriteLine("INVALID COMMAND - No Robot placed on the board");
                    result = GlobalVars.com_INVALID;
                }
            }
            else if (String.Equals(input.command,GlobalVars.com_LEFT, StringComparison.OrdinalIgnoreCase) == true){
                if (GlobalVars.TrList.Any() == true){
                    switch (GlobalVars.TrList.Last().direction){
                        case (GlobalVars.dir_NORTH):
                            GlobalVars.TrList.Last().direction = GlobalVars.dir_WEST;
                            break;
                        case (GlobalVars.dir_WEST):
                            GlobalVars.TrList.Last().direction = GlobalVars.dir_SOUTH;
                            break;
                        case (GlobalVars.dir_SOUTH):
                            GlobalVars.TrList.Last().direction = GlobalVars.dir_EAST;
                            break;
                        case (GlobalVars.dir_EAST):
                            GlobalVars.TrList.Last().direction = GlobalVars.dir_NORTH;
                            break;
                    }
                    result = input.command;
                }
                else{
                    Console.WriteLine("INVALID COMMAND - No Robot placed on the board");
                    result = GlobalVars.com_INVALID;
                }
            }
            else if (String.Equals(input.command,GlobalVars.com_RIGHT, StringComparison.OrdinalIgnoreCase) == true){
                if (GlobalVars.TrList.Any() == true){
                    switch (GlobalVars.TrList.Last().direction){
                        case (GlobalVars.dir_NORTH):
                            GlobalVars.TrList.Last().direction = GlobalVars.dir_EAST;
                            break;
                        case (GlobalVars.dir_WEST):
                            GlobalVars.TrList.Last().direction = GlobalVars.dir_NORTH;
                            break;
                        case (GlobalVars.dir_SOUTH):
                            GlobalVars.TrList.Last().direction = GlobalVars.dir_WEST;
                            break;
                        case (GlobalVars.dir_EAST):
                            GlobalVars.TrList.Last().direction = GlobalVars.dir_SOUTH;
                            break;
                    }
                    result = input.command;
                }
                else{
                    Console.WriteLine("INVALID COMMAND - No Robot placed on the board");
                    result = GlobalVars.com_INVALID;
                }

            }
            else if (String.Equals(input.command,GlobalVars.com_REPORT, StringComparison.OrdinalIgnoreCase) == true){
                if (GlobalVars.TrList.Any() == true){
                    Console.WriteLine("Current Position: " + GlobalVars.TrList.Last().xPosition + ", " + GlobalVars.TrList.Last().yPosition + ", " + GlobalVars.TrList.Last().direction);
                    result = input.command;
                }
                else{
                    Console.WriteLine("INVALID COMMAND - No Robot placed on the board");
                    result = GlobalVars.com_INVALID;
                }
            }
            else{
                Console.WriteLine("INVALID COMMAND");
                result = GlobalVars.com_INVALID;
            }

            return result;
        }
        public void drawBoard(List<ToyRobot> input){
            int i,j,drawYPos = GlobalVars.yLimitPos-1;

            for (i=0;i<GlobalVars.xLimitPos;i++){
                for (j=0;j<GlobalVars.yLimitPos;j++){
                    if (input.Last().yPosition == drawYPos && input.Last().xPosition == j){
                        switch(input.Last().direction){
                            case GlobalVars.dir_NORTH:
                                Console.Write("^ ");
                                break;
                            case GlobalVars.dir_SOUTH:
                                Console.Write("v ");
                                break;
                            case GlobalVars.dir_EAST:
                                Console.Write("> ");
                                break;
                            case GlobalVars.dir_WEST:
                                Console.Write("< ");
                                break;
                        }
                    }
                    else
                        Console.Write("o ");
                }
                drawYPos -= 1;
                Console.WriteLine("");
            }
        }
    }


    class Program{   
        static void Main(string[] args){
            TROperations trOperations = new TROperations();
            ToyRobot toyrobot = null;
            GlobalVars.TrList = new List<ToyRobot>();
            string finput;
            string operationDone;

            if (args.Any()){
                var path = args[0];
                if (File.Exists(path)){
                    System.IO.StreamReader objectReader;
                    objectReader = new System.IO.StreamReader(path);
                    while ((finput = objectReader.ReadLine()) != null)
                    {
                        if (GlobalVars.TrList.Any() == true)
                            trOperations.drawBoard(GlobalVars.TrList);
                        Console.WriteLine("Available commands:");
                        Console.WriteLine("PLACE X,Y,F");
                        if (GlobalVars.TrList.Any() == true){
                            Console.WriteLine("MOVE");
                            Console.WriteLine("LEFT");
                            Console.WriteLine("RIGHT");
                            Console.WriteLine("REPORT");
                        }
                        Console.WriteLine("EXIT");
                            
                        Console.WriteLine("------Enter command:");
                        

                        TRCommand trCommand = new TRCommand();
                        Console.WriteLine(finput);
                        trCommand = trOperations.validateInput(finput);

                        if (String.Equals(trCommand.command,GlobalVars.com_EXIT, StringComparison.OrdinalIgnoreCase) == true)
                            break;
                        else 
                            operationDone = trOperations.performCommand(trCommand, toyrobot);
                    }
                }
                else{
                    Console.WriteLine("Invalid file provided in arguments");
                }
            }
            else{
                while (true){
                    if (GlobalVars.TrList.Any() == true)
                        trOperations.drawBoard(GlobalVars.TrList);
                    Console.WriteLine("Available commands:");
                    Console.WriteLine("PLACE X,Y,F");
                    if (GlobalVars.TrList.Any() == true){
                        Console.WriteLine("MOVE");
                        Console.WriteLine("LEFT");
                        Console.WriteLine("RIGHT");
                        Console.WriteLine("REPORT");
                    }
                    Console.WriteLine("EXIT");
                        
                    Console.WriteLine("------Enter command:");
                    string? input = Console.ReadLine();

                    TRCommand trCommand = new TRCommand();
                    trCommand = trOperations.validateInput(input);

                    if (String.Equals(trCommand.command,GlobalVars.com_EXIT, StringComparison.OrdinalIgnoreCase) == true)
                        break;
                    else 
                        trOperations.performCommand(trCommand, toyrobot);
                }
            }
        }
    }

}
