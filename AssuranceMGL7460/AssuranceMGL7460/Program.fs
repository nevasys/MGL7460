// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.


// Useful linkf for us Alex:
// http://fsharp.github.io/FSharp.Data/library/JsonProvider.html
// https://msdn.microsoft.com/visualfsharpdocs/conceptual/visual-fsharp
module Program

open FSharp.Data;
open FSharp.Linq;

open JsonManager;

let jsonManager = new JsonManager("../../../Data/Dossier1.json")
jsonManager.PrintDoc()

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code
