// MIT License Copyright (c) 2020 timotheyca
// https://gitea.ongoteam.net/timofey/solve_poly/src/branch/master/LICENSE

#define LINE_HOOK
#define ODD_HOOK
using System.Collections.Generic;
using static System.Double;

namespace TMGmod.SolvePoly
{
    public enum SolutionCount
    {
        Finite,
        Infinite
    }

    internal struct Solution
    {
        private readonly uint _j;
        private readonly double[] _solution;

        internal Solution(uint i, double[] solution)
        {
            _j = i;
            _solution = solution;
        }

        internal double this[uint i]
        {
            get => _solution[_j + i];
            set => _solution[_j + i] = value;
        }

        internal Solution Descend()
        {
            return new Solution(_j + 1, _solution);
        }
    }

    internal struct Poly
    {
        private readonly uint _j;
        private readonly double[] _coeff;
        private uint _n;

        internal Poly(uint i, double[] coeff)
        {
            _j = i;
            _coeff = coeff;
            _n = (uint)(_coeff.Length - _j - 1);
        }

        private double this[uint i]
        {
            get => _coeff[_j + i];
            set => _coeff[_j + i] = value;
        }

        private Poly Descend()
        {
            return new Poly(_j + 1, _coeff);
        }

        private void DeRoot(double x)
        {
            for (var i = _n - 1; i > 0; --i) this[i] += x * this[i + 1];
        }

        private void ReRoot(double x)
        {
            for (uint i = 1; i < _n; ++i) this[i] -= x * this[i + 1];
        }

        private void Differentiate()
        {
            for (uint i = 1; i <= _n; ++i) this[i] *= i;
        }

        private void Integrate()
        {
            for (uint i = 1; i <= _n; ++i) this[i] /= i;
        }

        internal double Apply(double x)
        {
            double res = 0;
            for (var i = _n; i > 0; --i) res = (res + this[i]) * x;
            return res + this[0];
        }

        internal SolutionCount Solve(Solution solution, ref uint roots)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            while (_n > 0 && this[_n] == 0) solution[--_n] = NaN;
            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (_n == 0)
            {
                roots = 0;
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                return this[0] == 0 ? SolutionCount.Infinite : SolutionCount.Finite;
            }
#if LINE_HOOK
            if (_n == 1)
            {
                roots = 1;
                solution[0] = -this[0] / this[1];
            }
#endif
#if ODD_HOOK
            if (_n % 2 == 1)
            {
                DeRoot(solution[0] = new Bisection(-MaxValue, MaxValue, this).Perform());
                Descend().Solve(solution.Descend(), ref roots);
                ReRoot(solution[0]);
                roots += 1;
                for (uint i = 0; i < _n - 1; ++i)
                    if (solution[i] > solution[i + 1])
                    {
                        var buf = solution[i];
                        solution[i] = solution[i + 1];
                        solution[i + 1] = buf;
                    }

                return SolutionCount.Finite;
            }
#endif
            Differentiate();
            Descend().Solve(solution.Descend(), ref roots);
            Integrate();
            solution[0] = -MaxValue;
            uint j = 0;
            for (uint i = 0; i < roots + 1; ++i)
            {
                var upper = i < roots ? solution[i + 1] : MaxValue;
                var bisection = new Bisection(solution[i], upper, this);
                if (!bisection.Able) continue;
                var root = bisection.Perform();
                if (!IsNaN(root)) solution[j++] = root;
            }

            for (var i = j; i < _n; ++i) solution[i] = NaN;
            roots = j;
            return SolutionCount.Finite;
        }
    }

    internal struct Bisection
    {
        private readonly double _lower;
        private readonly double _upper;
        private Poly _poly;
        private readonly bool _asc;
        private readonly bool _dsc;
        internal bool Able => _asc || _dsc;

        internal Bisection(double lower, double upper, Poly poly)
        {
            _lower = lower;
            _upper = upper;
            _poly = poly;
            var lowerv = poly.Apply(lower);
            var upperv = poly.Apply(upper);
            _asc = lowerv <= 0 && 0 <= upperv;
            _dsc = lowerv >= 0 && 0 >= upperv;
        }

        internal double Perform()
        {
            var lower = _lower;
            var upper = _upper;
            if (_asc)
            {
                for (uint i = 2048; i > 0; --i)
                {
                    var mid = (lower + upper) / 2;
                    if (0 <= _poly.Apply(mid)) upper = mid;
                    else lower = mid;
                }

                return lower;
            }

            if (!_dsc) return NaN;
            for (uint i = 2048; i > 0; --i)
            {
                var mid = (lower + upper) / 2;
                if (0 >= _poly.Apply(mid)) upper = mid;
                else lower = mid;
            }

            return lower;
        }
    }

    public static class Solver
    {
        public static IEnumerable<double> Solve(double[] coeff)
        {
            var n = (uint)coeff.Length - 1;
            var solution = new double[n];
            uint roots = 0;
            var poly = new Poly(0, coeff);
            poly.Solve(new Solution(0, solution), ref roots);
            var res = new double[roots];
            for (uint i = 0; i < roots; i++) res[i] = solution[i];

            return res;
        }

        // ReSharper disable once UnusedMember.Global
        public static double Apply(double[] coeff, double x)
        {
            return new Poly(0, coeff).Apply(x);
        }
    }
}
