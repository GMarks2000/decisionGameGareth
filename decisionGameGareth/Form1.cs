/*This program was created by Gareth Marks
 * Starting October 14
 * As a text-based murder mystery adventure game*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Threading;

namespace decisionGameGareth
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            outputLabel.Text = "You wake up in a strange and unfamiliar room. You can hear rain pouring outside but there are no windows or furniture, only a single door. Do you try to open the door?";
            option1Label.Text = "Yes";
            option2Label.Text = "No";
            option3Label.Text = "";
            SoundPlayer rain = new SoundPlayer(Properties.Resources.rainSound);
            rain.Play();
        }
        int scene = 2; //tracks where the user is in the game
        Boolean murdererIdentityKnown = false;
        Boolean murderMethodKnown = false;

        //random numbers for use in random sections
        Random rand = new Random();

        // Makes screen flash red then black five times. Plays whenever player dies.
        void deathFlash()
        {
            for (int x = 0; x < 5; x++)
            {
                this.BackgroundImage = null;
                this.BackColor = Color.Black;
                Refresh();
                Thread.Sleep(100);
                this.BackColor = Color.Red;
                Refresh();
                Thread.Sleep(100);
            }
            this.BackColor = Color.Black;
        }

        

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.M)       //left option
            {
                if (scene == 0) { scene = 1; }//open door
                else if (scene == 1) { scene = 3; } //Go left in hallway
                else if (scene == 2) { scene = 0; }//death
                else if (scene == 3) { scene = 8; }//Run directly to stairwell
                else if (scene == 4) { scene = 13; }//get to bottom of stairwell
                else if (scene == 5) { scene = 0; }
                else if (scene == 6)
                {   //Escape rubble attempt. Generates random number 1-10 and kills playern if less than 6 
                    int collapseRand = rand.Next(1, 11);
                    if (collapseRand > 3)
                    {
                        scene = 12;
                    }
                    else
                    {
                        scene = 11;
                    }
                    murdererIdentityKnown = true; //stores that player knows who murderer is
                }
                else if (scene == 8) { scene = 13; }//continue to ground floor                
                else if (scene == 10)
                { //investigate corpse 
                    scene = 8;
                    murderMethodKnown = true;//stores that user knows murder method
                }
                else if (scene == 11) { scene = 0; }//death
                else if (scene == 12) { scene = 13; }//continue to ground floor
                else if (scene == 13) { scene = 14; }//Left in tunnel
                else if (scene == 14) { scene = 17; }//Left drawer
                else if (scene == 15)
                {
                    if (murdererIdentityKnown == true)//allows user to survive only if they have learned the murderers identity or murder method
                    {
                        scene = 20;
                    }
                    else if (murderMethodKnown == true)
                    {
                        scene = 21;
                    }
                    else { scene = 30; }
                }
                else if (scene == 16) { scene = 35; }//death
                else if (scene == 17)
                {
                    scene = 24; //escape to flat
                    murdererIdentityKnown = true; //stores that user knows murderers identity
                }//escape to flat
                else if (scene == 18) { scene = 24; }
                else if (scene == 19) { scene = 0; }//death
                else if (scene == 20) { scene = 24; }//possibility for encounter with murderer
                else if (scene == 21) { scene = 24; }//possibility for encounter with murderer
                else if (scene == 22) { scene = 0; }//possibility for encounter with murderer. Death (no info about murderer known).
                else if (scene == 23) { scene = 0; }//death
                else if (scene == 24)
                {   //determines output dependant on player knowledge. If player has failed to gain information they die.
                    if (murdererIdentityKnown == true && murderMethodKnown == true) { scene = 26; }
                    else if (murdererIdentityKnown == true) { scene = 27; }
                    else if (murderMethodKnown == true) { scene = 29; }
                    else { scene = 30; }
                }
                else if (scene == 25) { scene = 0; }//death
                else if (scene == 26 || scene == 27 || scene == 29) { scene = 31; }//tackle roberts
                else if (scene == 30) { scene = 0; }//death
                else if (scene == 31)
                {
                    if (murdererIdentityKnown == true && murderMethodKnown == true) { scene = 37; }//awards user perfect ending
                    else { scene = 38; }//win, but imperfect ending
                }
                else if (scene == 33) { scene = 0; }//win, but imperfect ending
                else if (scene == 34) { scene = 0; }//death
                else if (scene == 35) { scene = 0; }//death
                else if (scene == 36) { scene = 0; }//death
                else if (scene == 37) { scene = 0; }//perfect ending
                else if (scene == 38) { scene = 0; }//win but imperfect ending
                else if (scene == 39) { scene = 40; }//successful arrow catch. Leads to easter egg ending
                else if (scene == 40) { scene = 0; }//"easter egg" ending
                else if (scene == 41) { scene = 0; }//death
                else { }




            }
            else if (e.KeyCode == Keys.B)       //middle option
            {
                if (scene == 0) { scene = 2; }
                else if (scene == 1)
                {
                    //Scale wall attempt. Generates random number 1-10 and kills user if less than 6
                    int wallRand = rand.Next(1, 11);
                    if (wallRand > 5) { scene = 4; }//leads to ground floor
                    else { scene = 5; }//leads to death                                 
                }
                else if (scene == 3) { scene = 10; }
                else if (scene == 6) { scene = 11; }//seek cover. Leads to certain doom.
                else if (scene == 24) { scene = 25; }//ignore murderer. Leads to certain doom.
                else if (scene == 13) { scene = 15; }//break down door
                else if (scene == 14) { scene = 18; }//ignore both drawers
                else if (scene == 16) { scene = 24; }//don't investigate skeletons. Player survives.
                else if (scene == 23) { scene = 24; }//attempt to open other drawer. Leads to death.
                else if (scene == 26 || scene == 27 || scene == 29)//dodge arrow
                {
                    int dodgeRand = rand.Next(1, 11);// Dodge arrow attempt. Allows player to live if a random number is greater than 4.
                    if (dodgeRand > 4) { scene = 33; }//leads to victory (imperfect)
                    else { scene = 34; }//leads to death
                }

                else if (scene == 39) { scene = 41; }
                else if (scene == 2||scene == 5||scene == 11||scene == 19||scene == 22||scene == 25||scene == 30||scene == 33||scene == 34||scene == 35||scene == 36||scene == 37||scene == 38||scene == 40||scene == 41)//option to quit
                {
                    Close();
                }
                else { }
               
            }
            
            else if (e.KeyCode == Keys.N)       //right option
            {
                if (scene == 1) { scene = 6; }
                else if (scene == 13) { scene = 16; }//right in tunnel
                else if (scene == 14) { scene = 19; }//right drawer. Death
                else if (scene == 26 || scene == 27 || scene == 29)//catch arrow attempt. Allows user to live only if a random number 1-10 is greater than 8.
                {
                    int catchRand = rand.Next(1, 11);
                    if (catchRand > 8) { scene = 39; }//Success case. Leads to special "hidden" ending.
                    else { scene = 36; }//failure case. Leads to death.
                }
                else { }
            }
            //soundplayer definitions
            SoundPlayer rain = new SoundPlayer(Properties.Resources.rainSound);
            SoundPlayer thunderClap = new SoundPlayer(Properties.Resources.thunderclap);
            SoundPlayer caveInSound = new SoundPlayer(Properties.Resources.caveIn);
            SoundPlayer arrowFireSound = new SoundPlayer(Properties.Resources.arrowFire);
            SoundPlayer spookyEffect = new SoundPlayer(Properties.Resources.spookyEffect);
            SoundPlayer scream = new SoundPlayer(Properties.Resources.scream);
            SoundPlayer grenade = new SoundPlayer(Properties.Resources.grenadeSound);
            SoundPlayer rainEnding = new SoundPlayer(Properties.Resources.rainEnding);
            SoundPlayer smashEffect = new SoundPlayer(Properties.Resources.smashing);
            SoundPlayer painSound = new SoundPlayer(Properties.Resources.pain);

            //determines output based on scene
            switch (scene)
            {
                case 0:
                    outputLabel.Text = "You wake up in a strange and unfamiliar room. You can hear rain pouring outside but there are no windows or furniture, only a single door. Do you try to open the door?";
                    option1Label.Text = "Yes";
                    option2Label.Text = "No";                  
                    rain.Play();
                    this.BackgroundImage = Properties.Resources.spookyDoor;
                    murdererIdentityKnown = false;
                    murderMethodKnown = false;
                    break;
                case 1:
                    outputLabel.Text = "The door creaks open, revealing a dark hallway and a fork in the road. As soon as you leave you hear the roof of the room you just left collapse, exposing the night sky. Do you turn left, right, or try to escape through the hole in the ceiling of the last room?";
                    option1Label.Text = "Left";
                    option2Label.Text = "Scale Wall";
                    option3Label.Text = "Right";
                    this.BackgroundImage = Properties.Resources.spookyHall;//changes to hallway theme
                    thunderClap.Play();
                    break;
                case 2:
                    outputLabel.Text = "You stay in bed, frigthened of opening the door, until the roof collapses, killing you insantly.";
                    option1Label.Text = "Restart";
                    option2Label.Text = "Quit";
                    option3Label.Text = "";
                    caveInSound.Play();
                    deathFlash();
                    break;
                case 3:
                    outputLabel.Text = "You continue walking down the pitch-black hall until you feel something squishy at your feet. After fumbling for your phone's screen light, you find a dead body, still warm. You recognize it from a missing persons report. Do you run for your life or try to discover more about the body?";
                    option1Label.Text = "Run!";
                    option2Label.Text = "Investigate";
                    option3Label.Text = "";
                    spookyEffect.Play();
                    break;
                case 4:
                    outputLabel.Text = "You successfully scale the wall, but find it is too high to jump from to the ground below. However, by sidling along the roof, you are able to reach a trapdoor that takes you down a spiral staircase to the bottom floor of the house";
                    option1Label.Text = "Continue";
                    option2Label.Text = "";
                    option3Label.Text = "";
                    this.BackgroundImage = Properties.Resources.rainySky;//changes to outdoor theme
                    rainEnding.Play();
                    break;
                case 5:
                    outputLabel.Text = "You successfully scale the wall, but find that the wall is too high to jump from to the ground below. Still drowsy, you slip and fall to your death.";
                    option1Label.Text = "Restart";
                    option2Label.Text = "Quit";
                    option3Label.Text = "";
                    scream.Play();
                    this.BackgroundImage = Properties.Resources.rainySky;//changes to outdoor theme
                    Refresh();
                    Thread.Sleep(1000);
                    deathFlash();
                    break;
                case 6:
                    outputLabel.Text = "You continue down a brightly lit hallway until you stumble across a large painting of a wealthy-looking man named H.A. Roberts holding a bow and arrow. He appears to be the owner of the house. Spray-painted across the picture in bright red is the word 'Murderer' Suddenly the floor rumbles violently. Do you run or seek cover?";
                    option1Label.Text = "Run!";
                    option2Label.Text = "Seek cover";
                    option3Label.Text = "";
                    spookyEffect.Play();
                    murdererIdentityKnown = true;
                    break;
                case 8:
                    outputLabel.Text = "You run until you come across a staircase taking you to the bottom floor of the house.";
                    option1Label.Text = "Continue";
                    option2Label.Text = "";
                    option3Label.Text = "";
                    break;
                case 10:
                    outputLabel.Text = "You calmly overturn the corpse. Using your experience as a doctor, you determine the cause of death to be an arrow to the victim's side tipped with potent poison. You snap a picture with your phone's camera. Beginning to fear for your life, you start running.";
                    option1Label.Text = "Continue";
                    option2Label.Text = "";
                    option3Label.Text = "";
                    murderMethodKnown = true;
                    spookyEffect.Play();
                    break;
                case 11:
                    outputLabel.Text = "While tring to escape, you are crushed by falling debris. You are dead.";
                    option1Label.Text = "Restart";
                    option2Label.Text = "Quit";
                    option3Label.Text = "";
                    caveInSound.Play();
                    deathFlash();
                    break;
                case 12:
                    outputLabel.Text = "You escape to a stairwell just in time.";
                    option1Label.Text = "Continue";
                    option2Label.Text = "";
                    option3Label.Text = "";
                    caveInSound.Play();
                    break;
                case 13:
                    outputLabel.Text = "You reach the bottom floor of the house, only to find that the front door is barred. You also find a trap door by the front door leading to a mysterious tunnel that branches left and right. Do you turn left, right, or try to break down the door?";
                    option1Label.Text = "Left";
                    option2Label.Text = "Break down door";
                    option3Label.Text = "Right";
                    this.BackgroundImage = Properties.Resources.spookyHall;//changes to hallway theme
                    break;
                case 14:
                    outputLabel.Text = "You continue walking through the dank and dim tunnel until you stumble upon a rusty steel desk with two drawers. Which do you try to open?";
                    option1Label.Text = "Left";
                    option2Label.Text = "Neither";
                    option3Label.Text = "Right";
                    this.BackgroundImage = Properties.Resources.tunnel; //changes to tunnel theme
                    break;
                case 15:
                    outputLabel.Text = "You break down the door wih ease. In the front yard you see a shadowy figure with an unrecognizable object in his hands.";
                    option1Label.Text = "Look closer";
                    option2Label.Text = "";
                    option3Label.Text = "";
                    this.BackgroundImage = Properties.Resources.rainySky; //changes to outdoor theme
                    smashEffect.Play();
                    break;
                case 16:
                    outputLabel.Text = "You continue walking through the dank and dim tunnel until you stumble upon a brightly lit area. You notice a pile of a dozen skeletons, each with an arrow embedded in the ribcage. You recognize a potent poison on the arrowtips.Do you try to further investigate?";
                    option1Label.Text = "Yes";
                    option2Label.Text = "No";
                    option3Label.Text = "";
                    this.BackgroundImage = Properties.Resources.tunnel; //changes to tunnel theme
                    murderMethodKnown = true;
                    break;
                case 17:
                    outputLabel.Text = "You find an old newspaper clipping about how wealthy mansion owner H.A Roberts was suspected of murder before mysteriously vanishing. You recognize the doors in one of the pictures and realise it is his mansion you just escaped from.";
                    option1Label.Text = "Continue ";
                    option2Label.Text = "";
                    option3Label.Text = "";
                    spookyEffect.Play();
                    murdererIdentityKnown = true;
                    break;
                case 18:
                    outputLabel.Text = "You ignore the desk and keep walking";
                    option1Label.Text = "Continue ";
                    option2Label.Text = "";
                    option3Label.Text = "";
                    break;
                case 19:
                    outputLabel.Text = "You set off a booby trap bomb that kills you instantly.";
                    option1Label.Text = "Restart ";
                    option2Label.Text = "Quit";
                    option3Label.Text = "";
                    grenade.Play();
                    deathFlash();
                    break;
                case 20:
                    outputLabel.Text = "Moving closer, you recognize the figure as H.A Roberts from the painting earlier. Remembering that he was branded as a murderer, you start running, and escape just as something whizzes past your head.";
                    option1Label.Text = "Continue ";
                    option2Label.Text = "";
                    option3Label.Text = "";
                    arrowFireSound.Play();
                    break;
                case 21:
                    outputLabel.Text = "Moving closer, you recognize the item in his hands as a bow-- drawn in your direction!. Remembering the man murdered by a poisoned arrow, you start running, and escape just as an arrow whizzes past your head.";
                    option1Label.Text = "Continue ";
                    option2Label.Text = "";
                    option3Label.Text = "";
                    arrowFireSound.Play();
                    break;
                case 22:
                    outputLabel.Text = "You take a few steps closer, trying to make out the identity of the figure, when all of a sudden you are slain by an arrow to the forehead.";
                    option1Label.Text = "Restart ";
                    option2Label.Text = "Quit";
                    option3Label.Text = "";
                    arrowFireSound.PlaySync();
                    scream.Play();
                    deathFlash();
                    break;
                case 23:
                    outputLabel.Text = "Do you try the other drawer?";
                    option1Label.Text = "Yes";
                    option2Label.Text = "No";
                    option3Label.Text = "";
                    break;
                case 24:
                    outputLabel.Text = "You continue walking until you reach an exit to the tunnel. You are now in   grassy flat, with streetlights visible in the distance. You see a person in the distance. Do you try to approach them?";
                    option1Label.Text = "Yes";
                    option2Label.Text = "No";
                    option3Label.Text = "";
                    this.BackgroundImage = Properties.Resources.rainySky; //changes bg image to rainy sky
                    rainEnding.Play();
                    break;
                case 25:
                    outputLabel.Text = "You walk past, trying to get back to civilization. The man ignores you too, but once you are past, they shoot you in the head with a poisoned arrow. You are dead.";
                    option1Label.Text = "Restart";
                    option2Label.Text = "Quit";
                    option3Label.Text = "";
                    arrowFireSound.PlaySync();
                    scream.Play();
                    deathFlash();
                    break;
                case 26:
                    outputLabel.Text = "You recognize that the man is H.A Roberts, and see a bow tucked under his arm. He spots you exiting the tunnel, and reaches for an arrow. What do you do?";
                    option1Label.Text = "Try to tackle him";
                    option2Label.Text = "Try to dodge the arrow";
                    option3Label.Text = "Try to catch the arrow";
                    break;
                case 27:
                    outputLabel.Text = "You recognize that the man is H.A Roberts, the murderer. He spots you exiting the tunnel, and reaches for an unidentifiable object.  What do you do?";
                    option1Label.Text = "Try to tackle him";
                    option2Label.Text = "Try to dodge the arrow";
                    option3Label.Text = "Try to catch the arrow";
                    break;
                case 29:
                    outputLabel.Text = "You recognize a bow under the man's arm. He spots you exiting the tunnel, and reaches for an arrow.  What do you do?";
                    option1Label.Text = "Try to tackle him";
                    option2Label.Text = "Try to dodge the arrow";
                    option3Label.Text = "Try to catch the arrow";
                    break;
                case 30:
                    outputLabel.Text = "You can't make out who the man is or what he's holding until it's too late-- struck by an arrow, you are dead";
                    option1Label.Text = "Restart";
                    option2Label.Text = "Quit";
                    option3Label.Text = "";
                    arrowFireSound.PlaySync();
                    scream.Play();
                    deathFlash();
                    break;
                case 31:
                    outputLabel.Text = "You tackle your opponent before you can get a shot off. 'Curses!' He exclaims. 'I planned to hold you for ransom! You were never supposed to wake from my sedative so soon!' Bystanders from nearby soon arrive, and the police are called.";
                    option1Label.Text = "Continue";
                    option2Label.Text = "";
                    option3Label.Text = "";
                    painSound.Play();
                    break;
                case 33:
                    outputLabel.Text = "The arrow whizzes past your shoulder as you gracefully dodge it. Your adversary checks his quiver for another arrow but has run out. He runs, and you cannot catch him. You are safe, but police never catch H.A. Roberts, the mysterious bow murderer.";
                    option1Label.Text = "Play again?";
                    option2Label.Text = "Quit";
                    option3Label.Text = "";
                    arrowFireSound.Play();
                    break;
                case 34:
                    outputLabel.Text = "Your fumbled attempt at a dodge is too little and too late: the arrow strikes you squarely in the ribs, letting the poisoned tip slowly kill you.";
                    option1Label.Text = "Restart";
                    option2Label.Text = "Quit";
                    option3Label.Text = "";
                    arrowFireSound.PlaySync();
                    scream.Play();
                    deathFlash();
                    break;
                case 35:
                    outputLabel.Text = "You prick your finger on a poisoned arrow and die a slow and painful death.";
                    option1Label.Text = "Restart";
                    option2Label.Text = "Quit";
                    option3Label.Text = "";
                    painSound.Play();
                    deathFlash();
                    break;
                case 36:
                    outputLabel.Text = "You realize too late that attempting to catch the arrow was a terrible idea. It strikes you in the head and you are dead.";
                    option1Label.Text = "Restart";
                    option2Label.Text = "Quit";
                    option3Label.Text = "";
                    arrowFireSound.PlaySync();
                    scream.Play();
                    deathFlash();
                    break;
                case 37:
                    outputLabel.Text = "You are able to tell police that the man is notorious bow murderer H.A. Roberts and the location of his victim's bodies for police investigation. This allows for a speedier trial, and bow murderer H.A. Roberts is soon behind bars, thanks to you. Congrats on a perfect playthrough!";
                    option1Label.Text = "Play again?";
                    option2Label.Text = "Quit";
                    option3Label.Text = "";
                    this.BackgroundImage = Properties.Resources.policeNight;// changes to police theme
                    break;
                case 38:
                    outputLabel.Text = "You tell the police what you can, but it isn't enough to convict him on alone. An investigation of his mansion is planned, but while police are busy with other cases Roberts escapes custody. You are safe, but but police never catch H.A. Roberts, the mysterious bow murderer.";
                    option1Label.Text = "Play again?";
                    option2Label.Text = "Quit";
                    option3Label.Text = "";
                    this.BackgroundImage = Properties.Resources.policeNight;// changes to police theme
                    break;
                case 39:
                    outputLabel.Text = "Miraculously, you catch the arrow. Impressed, the shooter drops his bow and asks if you'd like to become his accomplice. What do you say?";
                    option1Label.Text = "Yes";
                    option2Label.Text = "No";
                    option3Label.Text = "";
                    arrowFireSound.Play();
                    break;
                case 40:
                    outputLabel.Text = "You become trained as sidekick to master bow murderer H.A. Roberts. With your combined talents, you are never caught by police and live out your lives comfortably. Congrats on finding the easter egg ending!";
                    option1Label.Text = "Play again?";
                    option2Label.Text = "";
                    option3Label.Text = "";
                    break;
                case 41:
                    outputLabel.Text = "Angered, the man beats you to death with his bow.";
                    option1Label.Text = "Restart";
                    option2Label.Text = "";
                    option3Label.Text = "";
                    scream.Play();
                    deathFlash();
                    break;
                default:
                    break;
            }
        }

        private void outputLabel_Click(object sender, EventArgs e)
        {

        }
    }
}