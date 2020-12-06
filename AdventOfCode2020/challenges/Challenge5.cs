using System;
using System.Linq;

namespace AdventOfCode2020.challenges
{
    public class Challenge5 : IChallenge
    {
        private readonly int _rowCount = 128;
        private readonly int _seatCount = 8;
        
        public long RunFirst()
        {
            var allLines = IChallenge.GetAllLines("5_1.txt");
            
            /*var maxSeatId = long.MinValue;
            foreach (var line in allLines)
            {
                var seatId = ParseSeatId(line);
                maxSeatId = seatId > maxSeatId ? seatId : maxSeatId;
            }
            return maxSeatId;*/
            
            //return allLines.Select(line => ParseSeatId(line)).Max();
            
            return allLines.Select(ParseSeatId).Max();
        }

        public long RunSecond()
        {
            throw new System.NotImplementedException();
        }

        public long ParseSeatId(string boardingPassCode)
        {

            var rowCode = boardingPassCode.Substring(0, 7);
            var seatCode = boardingPassCode.Substring(7);

            var row = GetRowNumber(rowCode);
            var seat = GetSeatNumber(seatCode); 

            return row * 8 + seat;
        }

        private long GetSeatNumber(string seatCode)
        {
            var firstSeat = 0;
            var lastSeat = _seatCount - 1;

            foreach (var character in seatCode)
            {
                (firstSeat,lastSeat) = GetNewSection(character, firstSeat, lastSeat);
            }

            return firstSeat;
        }

        private int GetRowNumber(string rowCode)
        {
            var firstRow = 0;
            var lastRow = _rowCount - 1;
            foreach (var character in rowCode)
            {
                (firstRow, lastRow) = GetNewSection(character, firstRow, lastRow);
            }

            var row = firstRow;
            return row;
        }

        private (int firstRow, int lastRow) GetNewSection(in char character, in int firstRow, in int lastRow)
        {
            int newFirstRow, newLastRow;
            switch (character)
            {
                case 'F':
                case 'L':
                    newFirstRow = firstRow;
                    newLastRow = firstRow + (lastRow - firstRow) / 2;
                    break;
                case 'B':
                case 'R':
                    newFirstRow = firstRow + (lastRow - firstRow) / 2 + 1;
                    newLastRow = lastRow;
                    break;
                default:
                    throw new ArgumentException();
            }

            return (newFirstRow, newLastRow);
        }
    }
}