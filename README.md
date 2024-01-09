# TerrariaMusicRandomizer
TMR is a music pack generator that picks random audio files from file/folder paths you specify.

### How to use
- Running the randomizer for the first time will create a 'Paths.txt' file.
- 'Paths.txt' is where you'll place any file/folder paths you want the randomizer to potentially use when generating a pack. Each path must be written on a separate line.
- Packs will be placed in a folder named 'Output'.

### Important Info
- This is not a mod, it is a standalone music pack generator, so it works with vanilla game or tmodloader
- Files are always copied from their original location, never moved or deleted.
- The randomizer only recognizes 'wav'/'ogg'/'mp3' extensions as audio files. (These are the only extensions terraria will recognize afaik)
- The randomizer will pick files until it either fills all song slots or runs out of files to pick from.
- If using a custom seed that consists of non-integer text, for example "Way Too Cool Seed B)", the text will be crushed down into an integer.

### Additional Info
- A '{PackName}_Spoiler.txt' file of song names will be placed inside the pack. The spoiler log will also be in the packs description viewable from in game.
- If you want to shuffle the vanilla ost you'll need to get those files yourself. You can extract music from the game using tools like TConvert.
