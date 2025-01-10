using System.Net.Security;
using System.Text.RegularExpressions;

namespace AdventOfCode._2015.Day16
{
    public readonly partial record struct Sue(int? Children, int? Cats, int? Samoyeds, int? Pomeranians, int? Akitas, int? Vizslas, int? Goldfish, int? Trees, int? Cars, int? Perfumes)
    {
        public Sue(string str) : this(
            Convert(str, ChildR()),
            Convert(str, CatR()),
            Convert(str, SamoyedR()),
            Convert(str, PomeranianR()),
            Convert(str, AkitaR()),
            Convert(str, VizslaR()),
            Convert(str, GoldfishR()),
            Convert(str, TreeR()),
            Convert(str, CarR()),
            Convert(str, PerfumeR())
        )
        {
        }

        private static int? Convert(string str, Regex pattern)
        {
            Match match = pattern.Match(str);
            return match.Success ? int.Parse(match.Value) : null;
        }

        public bool SimilarTo(Sue other)
        {
            if (Children != null && other.Children != Children) return false;
            if (Cats != null && other.Cats != Cats) return false;
            if (Samoyeds != null && other.Samoyeds != Samoyeds) return false;
            if (Pomeranians != null && other.Pomeranians != Pomeranians) return false;
            if (Akitas != null && other.Akitas != Akitas) return false;
            if (Vizslas != null && other.Vizslas != Vizslas) return false;
            if (Goldfish != null && other.Goldfish != Goldfish) return false;
            if (Trees != null && other.Trees != Trees) return false;
            if (Cars != null && other.Cars != Cars) return false;
            if (Perfumes != null && other.Perfumes != Perfumes) return false;
            return true;
        }

        public bool SimilarToReal(Sue other)
        {
            if (Children != null && other.Children != Children) return false;
            if (Cats != null && other.Cats >= Cats) return false;
            if (Samoyeds != null && other.Samoyeds != Samoyeds) return false;
            if (Pomeranians != null && other.Pomeranians <= Pomeranians) return false;
            if (Akitas != null && other.Akitas != Akitas) return false;
            if (Vizslas != null && other.Vizslas != Vizslas) return false;
            if (Goldfish != null && other.Goldfish <= Goldfish) return false;
            if (Trees != null && other.Trees >= Trees) return false;
            if (Cars != null && other.Cars != Cars) return false;
            if (Perfumes != null && other.Perfumes != Perfumes) return false;
            return true;
        }

        #region Regexes
        [GeneratedRegex(@"(?<=children: )\d+")]
        private static partial Regex ChildR();
        [GeneratedRegex(@"(?<=cats: )\d+")]
        private static partial Regex CatR();
        [GeneratedRegex(@"(?<=samoyeds: )\d+")]
        private static partial Regex SamoyedR();
        [GeneratedRegex(@"(?<=pomeranians: )\d+")]
        private static partial Regex PomeranianR();
        [GeneratedRegex(@"(?<=akitas: )\d+")]
        private static partial Regex AkitaR();
        [GeneratedRegex(@"(?<=vizslas: )\d+")]
        private static partial Regex VizslaR();
        [GeneratedRegex(@"(?<=goldfish: )\d+")]
        private static partial Regex GoldfishR();
        [GeneratedRegex(@"(?<=trees: )\d+")]
        private static partial Regex TreeR();
        [GeneratedRegex(@"(?<=cars: )\d+")]
        private static partial Regex CarR();
        [GeneratedRegex(@"(?<=perfumes: )\d+")]
        private static partial Regex PerfumeR();
        #endregion
    }
}
