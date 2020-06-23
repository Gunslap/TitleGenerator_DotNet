# TitleGenerator (.NetCore Library Edition + WebAPI)
My title generator PS script remade as a .NETCore library and accompanying web api for access.

Includes both the generic "GenerateTitle" method and the "GenerateStrikeForce" method from the original script and it's offshoot branch.

# How to Build:
-Build with Visual Studio 2019 (Community/Pro)

-If you just want to use the library, build only that project (skip the API)

# How to use the Library:
-Add a project reference to the assembly

-make sure you've kept the nouns.txt, verbs.txt and adjectives.txt files in the same directory as the .DLL (compiling should copy them in as well)

-create an instance of the TitleGenerator object

-call one of the available methods. Each returns a string:
  
## "GenerateTitle"
returns a randomly generated title name
  
## "GenerateStrikeForce"
returns a randomly generated STRIKE FORCE name (similar to "GenerateTitle, but has "Strike Force" on the front)
  
## "GenerateFromTemplate"
takes in a string and replaces each instance of "Noun", "Verb", and "Adjective" with a random word of that type and returns the modified string
  
## "GenerateNoun"
returns a noun with 0, 1, or 2 adjectives in front of it
  
## "GetNoun"/"GetVerb"/"GetAdjective"
returns a single random word read in from the text files of the specified type (useful for building your own random titles)

-update/modify the word list files as you see fit. They will be read in each time an instance of the TitleGenerator class is created

# How to use the Web API:
The following 4 REST routes are setup by default:

## "/TitleGenerator"
returns a strike force

## "/TitleGenerator/{int}"
returns {int} number of strike forces

## "/TitleGenerator/title
returns a default title name

## "/TitleGenerator/title/{int}"
returns {int} number of default title names

## "/TitleGenerator/ByTemplate/{string}
returns the string with the words "Noun", "Verb", and "Adjective" replaced with random words of those types

## "/TitleGenerator/ByTemplate/{string}/{int}
returns the string with the words "Noun", "Verb", and "Adjective" replaced with random words of those types {int} number of times

# Future Enhancements / TODO:
-(Library) make more helper methods to cut down on repeat code between GenerateTitle and GenerateStrikeForce functions

-(API) figure out how to allow .XML output as well but keep JSON as the default

-(API) creating some REST API metadata for the project
