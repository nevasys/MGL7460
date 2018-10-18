module JsonManager

open System.IO
open FSharp.Data;
open FSharp.Linq;

// this way is less dynamic but more stronger
//type Dossier1Json = FSharp.Data.JsonProvider<"../Data/Dossier1.json">
//   let JsonDoc1 = Dossier1Json.GetSample()
//   let mutable dossier = JsonDoc1.Dossier.ToString()       
//   let mutable mois = JsonDoc1.Mois.ToString()
//let filename = "../Data/Dossier1.json"

type JsonManager(filename) = class
   let Filename :string = filename

   let JsonDoc2 = JsonValue.Load(Filename)


   let mutable dossier = JsonDoc2.Item("dossier").ToString()    
   let mutable mois = JsonDoc2.Item("mois").ToString()

   member this.PrintDoc() =
     printfn "Json doc mois = %s" dossier 
     printfn "Json doc mois = %s" mois
 
     // just un example
//   member this.readLines = seq {
//    use sr = new StreamReader (Filename)
//    while not sr.EndOfStream do
//        yield sr.ReadLine ()
//   }
   
end

(*
type MyClass2(dataIn) as self =
    let data = dataIn
    do
        self.PrintMessage()
    member this.PrintMessage() =
        printf "Creating MyClass2 with Data %d" data
*)
