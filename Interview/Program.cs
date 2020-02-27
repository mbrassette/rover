// ======================================================================================
// Author: Michael Brassette
// Date: Tuesday, February 25th, 2020
// Notes: I am adding two assumptions that were not mentioned in the word document.
//        1) The rover's movement plan cannot have it go off the graph.  In that case,
//           that move action is discarded and the next action in the movement plan
//           is executed.
//        2) Only one rover can occupy a coordinate in the graph at one time.  If 
//           a rover tries to move where another rover is currently occupying, the move
//           action is discarded and the next action in the movement plan is executed.
// ======================================================================================

using System;
using System.Linq;

namespace SkyKick.Interview
{
    /// <summary>Program class</summary>
    class Program
	{
		static void Main(string[] args)
		{
            Boolean     CanProceed = true;
            Graph       GraphObject = null;
            Rover       RoverObject = null;

            // Prompt 1: "Enter Graph Upper Right Coordinate:"
            CanProceed = GetGraphUpperRightCoordinates(graphObject: ref GraphObject);

            // Variable CanProceed checks to see if the user has exited the program
            while (CanProceed)
            {
                // Prompt 2: "Rover # Starting Position:"
                if (CanProceed)
                    CanProceed = GetRoverStartingPosition(graphObject: ref GraphObject, roverObject: ref RoverObject);

                // Prompt 3: "Rover # Movement Plan:"
                if (CanProceed)
                    CanProceed = GetMovementPlan(graphObject: ref GraphObject, roverObject: ref RoverObject);

                // Display the output: "Rover # Output:"
                if (CanProceed)
                    DisplayOutput(graphObject: GraphObject, roverObject: RoverObject);
            }
        }

        /// <summary>Displays the X and Y coordinate of the rover on the graph and the current direction the rover is currenting heading</summary>
        /// <param name="graphObject">Graph object</param>
        /// <param name="roverObject">Rover object</param>
        private static void DisplayOutput(Graph graphObject, Rover roverObject)
        {
            roverObject.ProcessMovement(graphObject: graphObject);
            Console.WriteLine("Rover {0} Output: {1} {2} {3}", (graphObject.Rovers.Count()), roverObject.XCoordinate, roverObject.YCoordinate, roverObject.CurrentDirection);
        }

        /// <summary>Gets the graph upper right coordinates from the user</summary>
        /// <param name="graphObject">Graph object</param>
        /// <returns><see cref="Boolean">Boolean</see> indicating whether the user has exited the program</returns>
        private static Boolean GetGraphUpperRightCoordinates(ref Graph graphObject)
        {
            string[] SplitUserInput = null;

            Console.Write("Enter Graph Upper Right Coordinate (type Exit to quit anytime): ");
            SplitUserInput = Console.ReadLine().Split();

            while (true)
            {
                try
                {
                    // checks the number of entries is correct
                    if (!((SplitUserInput.Count() == 1 && SplitUserInput[0].ToUpper() == "EXIT") || (SplitUserInput.Count() == 2)))
                        throw new InvalidInputException("Invalid Input.  Enter Graph Upper Right Coordinate (type Exit to quit anytime): ");

                    if (SplitUserInput[0].ToUpper() != "EXIT")
                    {
                        // update the graph upper right coordinate properties of the graph object
                        graphObject = new Graph(Int32.Parse(SplitUserInput[0]), Int32.Parse(SplitUserInput[1]));
                    }

                    break;
                }
                catch (InvalidInputException)
                {
                    SplitUserInput = Console.ReadLine().Split();
                }
                catch (FirstEntryNotPositiveNumericException)
                {
                    SplitUserInput = Console.ReadLine().Split();
                }
                catch (SecondEntryNotPositiveNumericException)
                {
                    SplitUserInput = Console.ReadLine().Split();
                }
            }

            // return TRUE if the user has indicated to exit the program
            return !(SplitUserInput.Count() == 1 && SplitUserInput[0].ToUpper() == "EXIT");
        }

        /// <summary>Gets the movement plan for the rover from the user</summary>
        /// <param name="graphObject">Graph object</param>
        /// <param name="roverObject">Rover object</param>
        /// <returns><see cref="Boolean">Boolean</see> indicating whether the user has exited the program</returns>
        private static Boolean GetMovementPlan(ref Graph graphObject, ref Rover roverObject)
        {
            string[] SplitUserInput = null;

            Console.Write("Rover {0} Movement Plan (type Exit to quit anytime): ", (graphObject.Rovers.Count() + 1));
            SplitUserInput = Console.ReadLine().Split();

            while (true)
            {
                try
                {
                    // checks the number of entries is correct
                    if (!(SplitUserInput.Count() == 1))
                        throw new InvalidInputException("Invalid Input.  Rover " + (graphObject.Rovers.Count() + 1) + " Movement Plan (type Exit to quit anytime): ");

                    if (SplitUserInput[0].ToUpper() != "EXIT")
                    {
                        // update the movement plan property of the rover object
                        roverObject.UpdateMovementPlan(movementPlan: SplitUserInput[0], roverCount: graphObject.Rovers.Count());
                        // add the rover object to the list of rover objects that are currently on the graph
                        graphObject.Rovers.Add(item: roverObject);
                    }

                    break;
                }
                catch (InvalidInputException)
                {
                    SplitUserInput = Console.ReadLine().Split();
                }
                catch (FirstEntryNotValidMovementPlan)
                {
                    SplitUserInput = Console.ReadLine().Split();
                }               
            }

            // return TRUE if the user has indicated to exit the program
            return !(SplitUserInput.Count() == 1 && SplitUserInput[0].ToUpper() == "EXIT");
        }

        /// <summary>Gets the starting position and current direction of the rover from the user</summary>
        /// <param name="graphObject">Graph object</param>
        /// <param name="roverObject">Rover object</param>
        /// <returns><see cref="Boolean">Boolean</see> indicating whether the user has exited the program</returns>
        private static Boolean GetRoverStartingPosition(ref Graph graphObject, ref Rover roverObject)
        {
            string[] SplitUserInput = null;

            Console.Write("Rover {0} Starting Position (type Exit to quit anytime): ", (graphObject.Rovers.Count() + 1));
            SplitUserInput = Console.ReadLine().Split();

            while (true)
            {
                try
                {
                    // checks the number of entries is correct
                    if (!((SplitUserInput.Count() == 1 && SplitUserInput[0].ToUpper() == "EXIT") || (SplitUserInput.Count() == 3))) 
                        throw new InvalidInputException("Invalid Input.  Rover " + (graphObject.Rovers.Count() + 1) + " Starting Position (type Exit to quit anytime): ");

                    if (SplitUserInput[0].ToUpper() != "EXIT")
                    {
                        roverObject = new Rover(graphObject: graphObject, xCoordinate: Int32.Parse(SplitUserInput[0]), yCoordinate: Int32.Parse(SplitUserInput[1]), currentDirection: Char.Parse(SplitUserInput[2]));
                    }

                    break;
                }
                catch (InvalidInputException)
                {
                    SplitUserInput = Console.ReadLine().Split();
                }
                catch (FirstEntryNotPositiveNumericException)
                {
                    SplitUserInput = Console.ReadLine().Split();
                }
                catch (SecondEntryNotPositiveNumericException)
                {
                    SplitUserInput = Console.ReadLine().Split();
                }
                catch (ThirdEntryNotValidDirectionalCharacterException)
                {
                    SplitUserInput = Console.ReadLine().Split();
                }
                catch (FirstEntryNotWithinXCoordinateBoundaryException)
                {
                    SplitUserInput = Console.ReadLine().Split();
                }
                catch (SecondEntryNotWithinYCoordinateBoundaryException)
                {
                    SplitUserInput = Console.ReadLine().Split();
                }
                catch (RoverExistsOnXYCoordinateException)
                {
                    SplitUserInput = Console.ReadLine().Split();
                }
            }

            // return TRUE if the user has indicated to exit the program
            return !(SplitUserInput.Count() == 1 && SplitUserInput[0].ToUpper() == "EXIT");
        }
    }
}