using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MissilePuppy.TitleGenerator
{
    public class TitleGenerator
    {
        //Create Arrays to store our word lists:
        private string[] _Nouns;
        public string[] Nouns
        {
            get { return _Nouns; }
            set { _Nouns = value; }
        }
        private string[] _Adjectives;
        public string[] Adjectives
        {
            get { return _Adjectives; }
            set { _Adjectives = value; }
        }
        private string[] _Verbs;
        public string[] Verbs
        {
            get { return _Verbs; }
            set { _Verbs = value; }
        }
        //Random number generator
        private readonly Random rnd = new Random();

        //Constructor Method - initialize the word lists
        public TitleGenerator()
        {
            //Initialize the array lists

            
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //First check the current directory //string path = Directory.GetCurrentDirectory();
            if (File.Exists(@"nouns.txt"))
            {
                Nouns = System.IO.File.ReadAllLines(@"nouns.txt");
                Adjectives = System.IO.File.ReadAllLines(@"adjectives.txt");
                Verbs = System.IO.File.ReadAllLines(@"verbs.txt");
            }
            //then check the running assembly's directory
            else if(File.Exists(assemblyFolder + @"\nouns.txt"))
            {
                Nouns = System.IO.File.ReadAllLines(assemblyFolder + @"\nouns.txt");
                Adjectives = System.IO.File.ReadAllLines(assemblyFolder + @"\adjectives.txt");
                Verbs = System.IO.File.ReadAllLines(assemblyFolder + @"\verbs.txt");
            }
            else
            {
                //Can't find word lists
                throw new Exception("word files are missing.");
            }
        }
        /// <summary>
        /// returns a single random noun
        /// </summary>
        /// <returns></returns>
        public string GetNoun()
        {
            return Nouns[rnd.Next(0, Nouns.GetLength(0) - 1)];
        }

        /// <summary>
        /// returns a single random adjective
        /// </summary>
        /// <returns></returns>
        public string GetAdjective()
        {
            return Adjectives[rnd.Next(0, Adjectives.GetLength(0) - 1)];
        }

        /// <summary>
        /// returns a single random verb
        /// </summary>
        /// <returns></returns>
        public string GetVerb()
        {
            return Verbs[rnd.Next(0, Verbs.GetLength(0) - 1)];
        }

        /// <summary>
        /// returns a noun with 0, 1, or 2 adjectives in front of it
        /// </summary>
        /// <returns></returns>
        public string GenerateNoun()
        {
            string rNoun = GetNoun();
            int numOfAdjectives = rnd.Next(0, 9);
            //0-3 = one adjective - 40% chance
            if (numOfAdjectives < 3)
            {
                //get one adjective
                string adjective_1 = GetAdjective();
                //add it before the noun
                rNoun = adjective_1 + " " + rNoun;
            }
            //3 = two adjectives - 10% chance
            else if (numOfAdjectives < 4)
            {
                //get two adjectives
                string adjective_1 = GetAdjective();
                string adjective_2 = GetAdjective();
                //add it before the noun
                rNoun = adjective_1 + " " + adjective_2 + " " + rNoun;
            }
            //any other case (5 or above) is no adjectives - 50%
            //do nothing to the noun

            //return the noun + adjectives string
            return rNoun;
        }
        /// <summary>
        /// Returns a randomly generated Strike Force name
        /// </summary>
        /// <returns></returns>
        public string GenerateStrikeForce()
        {
            string title = ""; //the title we will return
            string noun1 = ""; //first noun in title
            string noun2 = ""; //potential second noun in title
            string verb1 = ""; //potential verb in title
            string adjective1 = ""; //potential adjective in title
            string endingAddition = ""; //potential sufix in title

            int titleStyle = rnd.Next(0, 8);
            switch (titleStyle)
            {
                //single noun
                case 0:
                    noun1 = GenerateNoun();
                    title = title + noun1;
                    break;
                //single noun... again
                case 1:
                    noun1 = GenerateNoun();
                    title = title + noun1;
                    break;
                //noun verb noun
                case 2:
                    noun1 = GenerateNoun();
                    verb1 = GetVerb();
                    noun2 = GenerateNoun();
                    title = title + noun1 + " " + verb1 + " " + noun2;
                    break;
                //verb noun
                case 3:
                    verb1 = GetVerb();
                    noun1 = GenerateNoun();
                    //TO DO: PS version has a maximum length of 40 characters check right here
                    title = title + verb1 + " " + noun1;
                    break;
                //Noun Verb Adjective
                case 4:
                    noun1 = GenerateNoun();
                    verb1 = GetVerb();
                    adjective1 = GetAdjective();
                    title = title + noun1 + " " + verb1 + " " + adjective1;
                    break;
                //Default will be "Noun Noun"
                default:
                    noun1 = GenerateNoun();
                    //We don't want the second noun to have any adjectives
                    //as those are weird looking on this type so we'll pull one directly from the array:
                    noun2 = GetNoun();
                    //TO DO: PS version has a maximum length of 40 characters check right here
                    title = title + noun1 + " " + noun2;
                    break;
            }
            //Now that we've got our general title structure
            //we will check it's lenth, if it's too long
            //skip even thinking of adding a suffix
            bool skipSuffix = false;
            if (title.Length > 30)
            {
                skipSuffix = true;
            }
            //Ending Modifier
            if (skipSuffix == false)
            {
                int endingModifyMode = rnd.Next(0, 50);
                switch (endingModifyMode)
                {
                    //sequels
                    case 0:
                        endingAddition = rnd.Next(2, 10).ToString();
                        break;
                    //year
                    case 6:
                        endingAddition = rnd.Next(1000, 3000).ToString();
                        break;
                    //Zero
                    case 8:
                        endingAddition = "Zero";
                        break;
                    //Unleashed
                    case 9:
                        endingAddition = "Unleashed";
                        break;
                    //Vengance
                    case 16:
                        endingAddition = "Vengance";
                        break;

                        //otherwise, do nothing!
                }
            }
            if (endingAddition != "")
            {
                title = title + " " + endingAddition;
            }
            //and last but not least, add STRIKE FORCE to the front
            title = "Strike Force " + title;
            //return the completed Title
            return title;
        }

        /// <summary>
        /// Returns a randomly generated media title name
        /// </summary>
        /// <returns></returns>
        public string GenerateTitle()
        {
            string title = ""; //the title we will return
            string noun1 = ""; //first noun in title
            string noun2 = ""; //potential second noun in title
            string verb1 = ""; //potential verb in title
            string adjective1 = ""; //potential adjective in title

            int titleStyle = rnd.Next(0, 8);
            switch (titleStyle)
            {
                //single noun
                case 0:
                    noun1 = GenerateNoun();
                    title = title + noun1;
                    break;

                //noun preposition noun
                case 1:
                    noun1 = GenerateNoun();
                    noun2 = GenerateNoun();
                    int joinerOp = rnd.Next(0, 6);
                    switch (joinerOp)
                    {
                        //Of
                        case 0:
                            title = noun1 + " of " + noun2;
                            break;
                        //In
                        case 1:
                            title = noun1 + " in " + noun2;
                            break;
                        //After
                        case 2:
                            title = noun1 + " After " + noun2;
                            break;
                        //Before
                        case 3:
                            title = noun1 + " Before " + noun2;
                            break;
                        //With
                        case 4:
                            title = noun1 + " With " + noun2;
                            break;
                        //Vs.
                        case 5:
                            title = noun1 + " Vs. " + noun2;
                            break;
                        //Featuring
                        default:
                            title = noun1 + " Featuring " + noun2;
                            break;
                    }
                    break;
                //noun verb noun
                case 2:
                    noun1 = GenerateNoun();
                    verb1 = GetVerb();
                    noun2 = GenerateNoun();
                    title = title + noun1 + " " + verb1 + " " + noun2;
                    break;
                //verb noun
                case 3:
                    verb1 = GetVerb();
                    noun1 = GenerateNoun();
                    //TO DO: PS version has a maximum length of 40 characters check right here
                    title = title + verb1 + " " + noun1;
                    break;
                //Noun Verb Adjective
                case 4:
                    noun1 = GenerateNoun();
                    verb1 = GetVerb();
                    adjective1 = GetAdjective();
                    title = title + noun1 + " " + verb1 + " " + adjective1;
                    break;
                //Default will be "Noun Noun"
                default:
                    noun1 = GenerateNoun();
                    //We don't want the second noun to have any adjectives
                    //as those are weird looking on this type so we'll pull one directly from the array:
                    noun2 = GetNoun();
                    //TO DO: PS version has a maximum length of 40 characters check right here
                    title = title + noun1 + " " + noun2;
                    break;
            }
            //Now that we've got our general title structure
            //we will check it's lenth, if it's too long
            //skip even thinking of adding a suffix
            bool skipSuffix = false;
            bool skipPrefix = false;
            if (title.Length > 30)
            {
                skipSuffix = true;
                skipPrefix = true;
            }
            //if it's over 20, skip 1 at random
            else if (title.Length > 20)
            {
                int skipper = rnd.Next(1);
                switch (skipper)
                {
                    case 0:
                        {
                            skipPrefix = true;
                            break;
                        }
                    case 1:
                        {
                            skipSuffix = true;
                            break;
                        }
                }
            }

            //Skip if too long
            if (skipPrefix == false)
            {
                int prefixMod = rnd.Next(0, 99);
                string prefixAdd = ""; //potential prefix in title

                //7 choices, even odds would be 14/100 each

                //Some of these make sense with a "The" in front of them
                //and others with a "The" after them. We will set a flag
                //to determine which "The" is appropriate
                bool theBefore = true;

                //"Super" - 10%
                if (prefixMod <= 9)
                {
                    prefixAdd = "Super ";
                }
                //"Ultimate" - 10%
                else if (prefixMod >= 10 & prefixMod <= 20)
                {
                    prefixAdd = "Ultimate ";
                }
                //"Extreme" - 7%
                else if (prefixMod >= 21 & prefixMod <= 28)
                {
                    prefixAdd = "Extreme ";
                }
                //"Legend of" - 8%
                else if (prefixMod >= 29 & prefixMod <= 37)
                {
                    prefixAdd = "Legend of ";
                }
                //"Age of" - 6%
                else if (prefixMod >= 38 & prefixMod <= 44)
                {
                    prefixAdd = "Age of ";
                }
                //"Imagine" - 5%
                else if (prefixMod >= 45 & prefixMod <= 50)
                {
                    prefixAdd = "Imagine ";
                    theBefore = false;
                }
                //"Revenge of" - 5%
                else if (prefixMod >= 51 & prefixMod <= 55)
                {
                    prefixAdd = "Revenge of ";
                    theBefore = false;
                }
                //44% chance - no prefix!

                //if a prefix was selected, add it in!
                if (prefixAdd != "")
                {
                    //Determine if we want a "The" in here and add the prefix
                    if (rnd.Next(0, 5) == 0 & title.Length <= 35)
                    {
                        //Can the "The" go before the prefix?
                        if (theBefore == true)
                        {
                            title = "The " + prefixAdd + title;
                        }
                        else
                        {
                            title = prefixAdd + "The " + title;
                        }
                    }
                    //No "The", just add the prefix
                    else
                    {
                        title = prefixAdd + title;
                    }
                }
                //No prefix, maybe we still want a "The"?
                else if (rnd.Next(0, 5) == 0 & title.Length <= 35)
                {
                    title = "The " + title;
                }
            }

            //Ending Modifier
            if (skipSuffix == false)
            {
                int endingModifyMode = rnd.Next(0, 50);
                string endingAddition = ""; //potential sufix in title
                switch (endingModifyMode)
                {
                    //sequels
                    case 0:
                        endingAddition = rnd.Next(2, 10).ToString();
                        break;
                    //year
                    case 6:
                        endingAddition = rnd.Next(1000, 3000).ToString();
                        break;
                    //Cronicles
                    case 7:
                        endingAddition = "Cronicles";
                        break;
                    //Zero
                    case 8:
                        endingAddition = "Zero";
                        break;
                    //Unleashed
                    case 9:
                        endingAddition = "Unleashed";
                        break;
                    //Origins
                    case 10:
                        endingAddition = "Origins";
                        break;
                    //Vengance
                    case 16:
                        endingAddition = "Vengance";
                        break;

                        //otherwise, do nothing!
                }
                if (endingAddition != "")
                {
                    title = title + " " + endingAddition;
                }
            }
            //return the completed Title
            return title;
        }

        /// <summary>
        /// Returns a randomly generated media title name based on the provided template
        /// example : "Noun Verb Adjective"
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public string GenerateTitleFromTemplate(string template)
        {
            //find and replace nouns
            while(template.Contains("Noun") == true)
            {
                int firstC = template.IndexOf("Noun");
                int lastC = template.IndexOf("Noun") + "Noun".Length;
                int endC = template.Length - (template.IndexOf("Noun") + "Noun".Length);
                string before = template.Substring(0, firstC);
                string after = template.Substring(lastC, endC);
                template = before + GetNoun() + after;
            }
            //find and replace verbs
            while (template.Contains("Verb") == true)
            {
                int firstC = template.IndexOf("Verb");
                int lastC = template.IndexOf("Verb") + "Verb".Length;
                int endC = template.Length - (template.IndexOf("Verb") + "Verb".Length);
                string before = template.Substring(0, firstC);
                string after = template.Substring(lastC, endC);
                template = before + GetVerb() + after;
            }
            //find and replace adjectives
            while (template.Contains("Adjective") == true)
            {
                int firstC = template.IndexOf("Adjective");
                int lastC = template.IndexOf("Adjective") + "Adjective".Length;
                int endC = template.Length - (template.IndexOf("Adjective") + "Adjective".Length);
                string before = template.Substring(0, firstC);
                string after = template.Substring(lastC, endC);
                template = before + GetAdjective() + after;
            }
            return template;
        }
    }
}