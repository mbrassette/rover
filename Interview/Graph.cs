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

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SkyKick.Interview
{
    /// <summary>Graph class</summary>
	public class Graph
	{
        /// <summary>Maximum X coordinate of the graph</summary>
        public int MaxXCoordinate { get; private set; }
        /// <summary>Maximum Y coordinate of the graph</summary>
        public int MaxYCoordinate { get; private set; }
        /// <summary>List of rovers that already have been placed on the graph</summary>
        public List<Rover> Rovers { get; set; }

        /// <summary>Initializes a new instance of the Graph class</summary>
        /// <param name="maxXCoordinate">Maximum X coordinate value of the graph</param>
        /// <param name="maxYCoordinate">Maximum Y coordinate value of the graph</param>
        public Graph(int maxXCoordinate, int maxYCoordinate)
		{
            // checks the first entry is a positive numeric with Regex
            if (!((Regex.IsMatch(maxXCoordinate.ToString(), @"^\d+$")) && (maxXCoordinate > 0)))
                throw new FirstEntryNotPositiveNumericException(message: "First entry or X max coordinate is not a positive numeric value.  Enter Graph Upper Right Coordinate (type Exit to quit anytime): ");
            // checks the second entry is a positive numeric with Regex
            if (!((Regex.IsMatch(maxYCoordinate.ToString(), @"^\d+$")) && (maxYCoordinate > 0)))
                throw new SecondEntryNotPositiveNumericException(message: "Second entry or Y max coordinate is not a positive numeric value.  Enter Graph Upper Right Coordinate (type Exit to quit anytime): ");

            // set the values of the properities of the graph instance
            this.MaxXCoordinate = maxXCoordinate;
			this.MaxYCoordinate = maxYCoordinate;
            Rovers = new List<Rover>();
        }
	}
}
