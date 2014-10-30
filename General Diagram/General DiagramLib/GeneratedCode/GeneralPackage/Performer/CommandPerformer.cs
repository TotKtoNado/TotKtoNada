﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан инструментальным средством
//     В случае повторного создания кода изменения, внесенные в этот файл, будут потеряны.
// </auto-generated>
//------------------------------------------------------------------------------
namespace GeneralPackage.Performer
{
	using GeneralPackage.GameData;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
    using GeneralPackage.Structures;

	public class CommandPerformer
    {
        #region attributes

        private readonly Board board;

        #endregion

        #region constructor

        public CommandPerformer(Board board)
        {
            this.board = board;
        }

        #endregion


        public bool makeStep(Coord direction, double speedPercent, int agentID) //отладить
        {
            if (speedPercent <= 0) {
                return false;
            }
            Coord movementVector;
            double speed = board.Agents.getDictionary()[agentID].speed;
            if (speedPercent > 1)
            {
                movementVector = new Coord(direction.x, direction.y);
            }
            else
            {
                movementVector = new Coord(direction.x * speedPercent * speed, 
                                           direction.y * speedPercent * speed);
            }
            if (isPathLegal(board.Agents[agentID].coord, board.Agents[agentID].coord + movementVector, agentID))
            {
                Coord temp = board.Agents[agentID].coord + movementVector;
                if (!temp.inField()) {
                    return false;
                }
                board.Agents[agentID].coord = temp;
                //System.Console.Write(board.Agents[agentID].coord.ToString());
                return true;
            }
            else
            {
                return false;
            }
        } 

        private bool isPathLegal(Coord start, Coord destination, int agentID) //отладить
        {
            Coord temp = new Coord();
            return !board.Walls.intersectsWalls(start, destination, out temp); //does not intersect walls
        }




    }
}
