using System.Collections.Generic;

namespace MatchUp.Shared
{
    public static class Constants
    {
        public static Dictionary<int, string> NamesRu = new Dictionary<int, string>
        {
            {1, "Характер"},
            {2, "Энергия"},
            {3, "Интерес к наукам"},
            {4, "Здоровье"},
            {5, "Логика, интуиция"},
            {6, "Склонность к труду, власти"},
            {7, "Везение, удача"},
            {8, "Чувство долга "},
            {9, "Ум, память "}
        };

        public static Dictionary<int, string> NamesEn = new Dictionary<int, string>
        {
            {1, "Character, will"},
            {2, "Vital energy"},
            {3, "Cognitive, creative"},
            {4, "Health, beauty"},
            {5, "Logic, intuition"},
            {6, "Labor, skill "},
            {7, "Luck"},
            {8, "Duty"},
            {9, "Intellect, memory"}
        };

        public static class Fb
        {
            public static string AccessToken = "EAAC2p2dqql4BABEbUPRyyBIQl79X5sZAzgjjZAEw0srCFInZB0KJ98O8KRPwHc02uqgfn2ML37GQztn5wVUQC4E1SvhYf1LpjrmotZBOoB6ZAlVqeRtiTZBV87KM6wCUoxBRuoinZBvZC0eFBmrZAVseYF9p69IlsCEom64ZCzFs6XiwZDZD";

            public static string UserQuery = "me/?fields=birthday,first_name,last_name,picture.height(100).width(100)";

            public static string FirstName = "first_name";
            public static string LastName = "last_name";
            public static string Birthday = "birthday";
            public static string Picture = "picture";
        }

    }
}