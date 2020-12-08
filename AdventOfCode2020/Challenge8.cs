using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Challenge8 : IChallenge
    {
        private string[] _instructions;
        private readonly HashSet<int> _set = new HashSet<int>();
        private int _accumulator;
        public long RunFirst()
        {
            _instructions = IChallenge.GetAllLines("8_1.txt");
            //_instructions = IChallenge.GetAllLines("test_8_1.txt");
            
            DoInstruction(0);
            return _accumulator;
        }

        private int DoInstruction(int instructionPointer)
        {
            if (!_set.Add(instructionPointer)) return -1;
            if(instructionPointer >= _instructions.Length) return 0;

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

        public long RunSecond()
        {
            //_instructions = IChallenge.GetAllLines("test_8_1.txt");
            _instructions = IChallenge.GetAllLines("8_1.txt");
            
            var nopInstrunction = "nop";
            var jmpInstruction = "jmp";
            var jmps = Enumerable.Range(0, _instructions.Length).Where(i => _instructions[i].Contains(jmpInstruction)).ToList();
            var nops = Enumerable.Range(0, _instructions.Length).Where(i => _instructions[i].Contains(nopInstrunction)).ToList();

            foreach (var jmp in jmps)
            {
                _set.Clear();
                _accumulator = 0;
                _instructions[jmp] = _instructions[jmp].Replace(jmpInstruction, nopInstrunction);
                if (DoInstruction(0) == 0)
                {
                    return _accumulator;
                }
                _instructions[jmp] = _instructions[jmp].Replace(nopInstrunction, jmpInstruction);
            }
            
            foreach (var nop in nops)
            {
                _set.Clear();
                _accumulator = 0;
                _instructions[nop] = _instructions[nop].Replace(nopInstrunction, jmpInstruction);
                if (DoInstruction(0) == 0)
                {
                    return _accumulator;
                }
                _instructions[nop] = _instructions[nop].Replace(jmpInstruction, nopInstrunction);
            }

            throw new Exception("Not path to end was found");
        }
    }
}