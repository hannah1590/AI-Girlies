# AI-Girlies
Hunt and Target with Probability Density Functions

We wanted to make an artificial intelligence that could play the strategy game Battleship. To do this we made two 10x10 grids for the player and AI. The AI randomly placed their ships, while the player could choose where their ships went.


Once the setup was complete the game would start and the player and AI would take turns firing on each other. We wanted to make an AI that could win as fast as possible so we decided to use the hunt and target algorithm with probability density functions. Normally, the hunt would shoot randomly until it hit something, however, this is not an optimized solution. Instead, we used probability maps to determine how likely each tile is to have a ship. After each shot, the probability map is updated to account for the shots already taken. If the shot hits a ship then the AI swaps from hunt mode to target mode, where until the ship is sunk the AI uses a stack of the four tiles that the ship possibly could be. It stays in this mode until the game informs it that the ship is sunk.
	

Our inspiration for developing our Battleship AI, particularly the incorporation of the Hunt and Target strategy coupled with Probability Maps, was drawn from the insightful and pioneering work of Data Genetics in their Battleship blog post. The Data Genetics blog not only provided a thorough analysis of different strategies in the classic game but also delved into the statistical nuances that underlie effective decision-making. We were particularly intrigued by the concept of Probability Maps as a dynamic tool for strategic gameplay. The idea of integrating probability into the decision-making process resonated with us, prompting a deeper exploration into how such an approach could elevate the sophistication of our AI algorithm. By building upon the foundations laid out by Data Genetics, we sought to create an AI that not only mimics human-like intuition but also adapts dynamically to the evolving challenges posed by the game. The inspiration derived from Data Genetics has been instrumental in shaping our approach, driving us to push the boundaries of Battleship AI and enhance the gaming experience for enthusiasts and competitors alike.

Link to Data Genetics Battleship blog post : http://www.datagenetics.com/blog/december32011/
