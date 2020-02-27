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
using System.Text.RegularExpressions;

namespace SkyKick.Interview
{
    /// <summary>Rover class</summary>
    public class Rover
	{
        /// <summary>X coordinate position of the rover</summary>
        public int XCoordinate { get; set; }
        /// <summary>Y coordinate position of the rover</summary>
        public int YCoordinate { get; set; }
        /// <summary>Current direction of the rover</summary>
        public char CurrentDirection { get; set; }
        /// <summary>Scheduled movement plan of the rover</summary>
        public string MovementPlan { get; set; }

        /// <summary>Initializes a new instance of the Graph class</summary>
        /// <param name="graphObject">Graph object that the rover will be associated</param>
        /// <param name="xCoordinate">X coordinate position of the rover</param>
        /// <param name="yCoordinate">Y coordinate position of the rover</param> 
        /// <param name="currentDirection">Current direction of the rover</param> 
        /// <param name="movementPlan">Movement plan of the rover on the graph</param>
        public Rover(Graph graphObject, int xCoordinate, int yCoordinate, char currentDirection, string movementPlan = "")
		{
            // checks the first entry is a positive numeric with Regex
            if (!((Regex.IsMatch(xCoordinate.ToString(), @"^\d+$")) && (xCoordinate > 0)))
                throw new FirstEntryNotPositiveNumericException(message: "Rover " + (graphObject.Rovers.Count() + 1) + "'s X coordinate is not a positive numeric value.  Enter Rover " + (graphObject.Rovers.Count() + 1) + " Starting Position (type Exit to quit anytime): ");
            // checks the second entry is a positive numeric with Regex
            if (!((Regex.IsMatch(yCoordinate.ToString(), @"^\d+$")) && (yCoordinate > 0)))
                throw new SecondEntryNotPositiveNumericException(message: "Rover " + (graphObject.Rovers.Count() + 1) + "'s Y coordinate is not a positive numeric value.  Enter Rover " + (graphObject.Rovers.Count() + 1) + " Starting Position (type Exit to quit anytime): ");
            // checks the directional entry is a valid letter
            if (!(Regex.IsMatch(currentDirection.ToString(), @"N|E|S|W|n|e|s|w")))
                throw new ThirdEntryNotValidDirectionalCharacterException(roverCount: graphObject.Rovers.Count() + 1);
            // checks the first entry fits within the graph's max x-coordinate range
            if (!(xCoordinate <= graphObject.MaxXCoordinate))
                throw new FirstEntryNotWithinXCoordinateBoundaryException(roverCount: graphObject.Rovers.Count() + 1);
            // checks the second entry fits within the graph's max y-coordinate range
            if (!(yCoordinate <= graphObject.MaxYCoordinate))
                throw new SecondEntryNotWithinYCoordinateBoundaryException(roverCount: graphObject.Rovers.Count() + 1);
            if (!(graphObject.Rovers.Where(graph => graph.XCoordinate == xCoordinate && graph.YCoordinate == yCoordinate).Count() == 0))
                throw new RoverExistsOnXYCoordinateException(roverCount: graphObject.Rovers.Count() + 1, xCoordinate: xCoordinate, yCoordinate: yCoordinate);

            // set the values of the properities of the rover instance
            this.XCoordinate = xCoordinate;
            this.YCoordinate = yCoordinate;
            this.CurrentDirection = Char.ToUpper(currentDirection);
            this.MovementPlan = movementPlan.ToUpper();
        }

        /// <summary>Changes the direction the rover is currently moving</summary>
        /// <param name="directionalChange">New direction that the rover will now head</param>
        public void ChangeDirection(char directionalChange)
        {
            switch (Char.ToUpper(CurrentDirection))
            {
                case 'N':
                    switch (Char.ToUpper(directionalChange))
                    {
                        case 'L':
                            CurrentDirection = 'W';
                            break;
                        case 'R':
                            CurrentDirection = 'E';
                            break;
                    }
                    break;
                case 'E':
                    switch (Char.ToUpper(directionalChange))
                    {
                        case 'L':
                            CurrentDirection = 'N';
                            break;
                        case 'R':
                            CurrentDirection = 'S';
                            break;
                    }
                    break;
                case 'S':
                    switch (Char.ToUpper(directionalChange))
                    {
                        case 'L':
                            CurrentDirection = 'E';
                            break;
                        case 'R':
                            CurrentDirection = 'W';
                            break;
                    }
                    break;
                case 'W':
                    switch (Char.ToUpper(directionalChange))
                    {
                        case 'L':
                            CurrentDirection = 'S';
                            break;
                        case 'R':
                            CurrentDirection = 'N';
                            break;
                    }
                    break;
            }
        }

        /// <summary>Moves the rover one coordinate in the direction of its current direction if applicable</summary>
        /// <param name="graphObject">Graph object that the rover will be associated</param>
        public void Move(Graph graphObject)
        {
            switch (Char.ToUpper(CurrentDirection))
            {
                case 'N':
                    if ((YCoordinate + 1 <= graphObject.MaxYCoordinate) // make sure within graph's boundaries
                        && (graphObject.Rovers.Where(graph => graph.XCoordinate == XCoordinate && graph.YCoordinate == YCoordinate + 1).Count() == 0)) // make sure no other rovers are occupying this space
                        YCoordinate++;
                    break;
                case 'E':
                    if ((XCoordinate + 1 <= graphObject.MaxXCoordinate) // make sure within graph's boundaries
                        && (graphObject.Rovers.Where(graph => graph.XCoordinate == XCoordinate + 1 && graph.YCoordinate == YCoordinate).Count() == 0)) // make sure no other rovers are occupying this space
                        XCoordinate++;
                    break;
                case 'S':
                    if ((YCoordinate - 1 >= 0)  // make sure within graph's boundaries
                        && (graphObject.Rovers.Where(graph => graph.XCoordinate == XCoordinate && graph.YCoordinate == YCoordinate - 1).Count() == 0)) // make sure no other rovers are occupying this space
                        YCoordinate--;
                    break;
                case 'W':
                    if ((XCoordinate - 1 >= 0)  // make sure within graph's boundaries
                        && (graphObject.Rovers.Where(graph => graph.XCoordinate == XCoordinate - 1 && graph.YCoordinate == YCoordinate).Count() == 0)) // make sure no other rovers are occupying this space
                        XCoordinate--;
                    break;
            }
        }

        /// <summary>Moves the rover around the graph based on the instructions of the process movement set for the rover</summary>
        /// <param name="graphObject">Graph object that the rover will be associated</param>
        public void ProcessMovement(Graph graphObject)
        {
            foreach (char c in MovementPlan)
            {
                switch (Char.ToUpper(c))
                {
                    case 'L':
                    case 'R':
                        ChangeDirection(directionalChange: c);
                        break;
                    case 'M':
                        Move(graphObject: graphObject);
                        break;
                }
            }
        }

        /// <summary>Moves the rover around the graph based on the instructions of the process movement</summary>
        /// <param name="movementPlan">Movement plan of the rover on the graph</param>
        /// <param name="roverCount">The rover number that is currently having its position moved on the graph</param>
        public void UpdateMovementPlan(string movementPlan, int roverCount)
        {
            // checks the movement characters are valid 
            if (!(Regex.IsMatch(movementPlan, @"^[LRMlrm]*$")))
                throw new FirstEntryNotValidMovementPlan(roverCount: roverCount + 1);

            this.MovementPlan = movementPlan.ToUpper();
        }
    }
}
