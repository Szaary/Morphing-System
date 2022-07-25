# TurnBasedGame
Framework for 2d rpg game. 

# Features
- Basic game structure with Zenject
- Fully customizable statistics. To add "inteligence" to character stats just create stat scriptable object "inteligence" and add to character statistics list.
- Turn based system with "ISubscribeToBattleStateChanged" interface, anything can subscribe to it to extend gameplay without changing base code.
- Multi Scene usage. Every terrain is scene, battle types are different scenes, hud is another. You can start game from any scene in editor. 
- GUI Creators for characters etc.
