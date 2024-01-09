# TerrariaMusicRandomizer
TMR is a music pack generator that picks random audio files from file/folder paths you specify.

### How to use
- Running the exe for the first time will create a 'Paths.txt' file.
  'Paths.txt' is where you'll place any file/folder paths you want the randomizer to potentially use when generating a pack.
  Each path must be written on a separate line.
- The randomizer only recognizes 'wav'/'ogg'/'mp3' extensions as audio files. (These are the only extensions terraria will recognize afaik)
- Packs will be placed in a folder named 'Output'.
  A 'Spoiler.txt' file of song names will be placed inside the pack.
  The spoiler log will also be in the packs description viewable from in game.

### Important Notes
- This is not a mod, it is a standalone music pack generator, so it works with vanilla game or tmodloader
- If using a custom seed that is text (not an integer), for example "Way Too Cool Seed B)", the text will be crushed down into an integer.
