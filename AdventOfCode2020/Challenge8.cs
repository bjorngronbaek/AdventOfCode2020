using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    public class Challenge8 : IChallenge
    {
        private string[] _instructions;
        private HashSet<int> _set = new HashSet<int>();
        private int _accumulator = 0;
        public long RunFirst()
        {
            _instructions = IChallenge.GetAllLines("8_1.txt");
            //_instructions = IChallenge.GetAllLines("test_8_1.txt");
            DoInstruction(0);
            return _accumulator;
        }

        private int DoInstruction(int instructionPointer)
        {
            if (_set.Add(instructionPointer))
            {
                var instruction = _instructions[instructionPointer];
                var split = instruction.Split(' ');
                switch (split[0])
                {
                    case "nop":
                        return DoInstruction(instructionPointer+1);
                    case "acc":
                        _accumulator += int.Parse(split[1]);
                        return DoInstruction(instructionPointer+1);
                    case "jmp":
                        return DoInstruction(instructionPointer+int.Parse(split[1]));
                    default:
                        throw new ArgumentException();
                }
            }

            return instructionPointer;
        }

        public long RunSecond()
        {
            throw new System.NotImplementedException();
        }
    }
}