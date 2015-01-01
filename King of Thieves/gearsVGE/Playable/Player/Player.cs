using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;

using Gears.Cloud._Debug;


namespace Gears.Playable
{
    
    public abstract class Player : Unit
    {
        public Player(Vector2 origin, Color color, float rotation/*, Vector2 imageOrigin/*, string textureFileName*/)
            : base(origin, color, rotation/*, imageOrigin, textureFileName*/) 
        {
            //InitializeLocal(); 
        }
        public Player(Vector2 origin, Color color, float rotation, Vector2 imageOrigin/*, string textureFileName*/)
            : base(origin, color, rotation, imageOrigin/*, textureFileName*/)
        {
            //InitializeLocal(); 
        }

        public virtual void Activate() { }
    }





    //EVERYTHING BELOW SHOULD NOT BE USED UNTIL IT IS COMPLETELY REFACTORED FROM A MODULAR STANDPOINT.
    //YOU HAVE BEEN WARNED :]
    //internal class PlayerManager
    //{
    //    private Player player;

    //    internal PlayerManager()
    //    {
    //        Initialize();
    //    }
    //    private void Initialize()
    //    {
    //        player = new Player();
    //    }

    //    private void ChangeEquipment(/*Equipment[] newStuff*/) //when the weapons and armor class are implemented, they can be derived from this class to make passing them easier.
    //    {
    //        //grab the type info of each element and change the equipment accordingly.
    //    }

    //    private void RemoveEquipment(/*Equipment[] remove*/)
    //    {

    //    }

    //    private void GiveItem(/*Item item, int quantity*/) //specify a negative quantity to remove
    //    {

    //    }

    //    /*private plstate CheckState()
    //     {
    //        return player.GetState();
    //     }
    //     */

    //    internal void Update(GameTime gameTime)
    //    {
    //        //check player events from here and execute appropriate handlers   
    //    }
    //    internal void Draw(SpriteBatch spriteBatch)
    //    {

    //    }
    //}
    //internal class Player
    //{
    //    bool IsAlive;
    //    Vector2 position;

    //    float baseAttack;
    //    float baseDefense;
    //    float defenseBonus = 10; //This will be based on specific armor worn.  For example, armored gloves will increase this. This is applied when guarding

    //    float health; //        life
    //    float stamina; //       energy: -2 <= stamina <= 2? Might want to work on this range
    //    float topspeed; //      
    //    Vector2 velocity;
    //    Vector2 accel; //         
    //    float controlradius; // radius that controlled STEVE: wat?

    //    //these methods need to be able to inteface with the input manager.
    //    void controlAccel() //acceleration is based on current stamina
    //    {
    //        //the stamina range will allow acceleration to be adjusted based on y = x^3 (probably will need to be adjusted)
    //        //as it approaches 0, the player will continue to lose acceleration.  
    //        //once it drops below 0, the player will deccelerate until they've returned to walking speed
    //        //accel.X = stamina * stamina * stamina;
    //    }

    //    void walk(){}
    //    void sprint()
    //    {
    //        stamina -= .2f;
    //        controlAccel();

    //        velocity.X += accel.X;
    //        velocity.Y += accel.Y;

    //        if (velocity.X > topspeed)
    //        {
    //            velocity.X = topspeed;
    //        }

    //        if (velocity.Y > topspeed)
    //        {
    //            velocity.Y = topspeed;
    //        }
    //        position.X += velocity.X;
    //        position.Y += velocity.Y;


    //    }

    //    double guard(float opposingAttack)
    //    {
    //        double def = baseDefense + defenseBonus; //10 is defense mode bonus, will change this later on
    //        double atk = Math.Sqrt(opposingAttack);

    //        if (def < atk)
    //            return (atk - def);

    //        return 0; //this occurs when the def is higher than the attack
            
    //    }
        
        
        
    //    double attack(float opposingDefense)
    //    {
            

    //        double def = opposingDefense, atk = baseAttack;

    //        def = Math.Sqrt(def);
    //        atk = .5 * Math.Pow(atk, .75);
    //        return (atk - def);

    //    }

    //    /*public plstate GetState()
    //     * {
    //     *  return state;
    //     *  }
    //     */

    //    //Ability[] abilities; //these should be bound to commands
    //    //Weapon[] weapon = new Weapon[1]; //check syntax. this is here to give the idea
    //    //Armor[] armor; //etc
    //    //Skill[] skills;
    //    //Item[] items;
    //    //PlState state; //Hidden, sneaking, compromised etc? This should be able to hold more than one state.  For example, a player can be both hidden as well as compromised
    //                     //Can also lead into special moves, such as a sneak attack
    //    //Item currentItem; //the item equipped
    //    //Weapon currentWeapon; //the weapon equipped

    //    internal Player()
    //    {

    //    }

    //}
}
