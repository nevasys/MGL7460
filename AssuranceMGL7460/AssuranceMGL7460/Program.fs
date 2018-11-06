module Program

open FSharp.Data;
open FSharp.Linq;
open JsonManager;
open JSONHash
open Xunit


//let jsonManager = new JsonManager("../../../Data/Dossier1.json")
//jsonManager.PrintDoc()

[<EntryPoint>]
let main argv = 
    if(argv.Length < 1) then printfn "argument missing"

    let fichiers = new JSONHash.fichierJSON(argv.[0], argv.[1])

    lireFichierReclamation(argv.[0].ToString(), argv.[1].ToString())
    

    0 // return an integer exit code

