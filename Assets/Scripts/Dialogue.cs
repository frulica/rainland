using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.CorgiEngine
{   

    [RequireComponent(typeof(Collider2D))]

    /// <summary>
    /// Add this class to an empty component. It will automatically add a boxcollider2d, set it to "is trigger". Then customize the dialogue zone
    /// through the inspector.
    /// </summary>

    [System.Serializable]
    public class Dialogue : DialogueZone 
    {   
        public Vector2 dialogue_offset = new Vector2(0, 0);

        public DialogueLine[] lines;

        private CorgiController _previusCharacter; 

        /// <summary>
        /// When triggered, either by button press or simply entering the zone, starts the dialogue
        /// </summary>
        public override void StartDialogue()
        {
            // if the button A prompt is displayed, we hide it
            if (_buttonA!=null)
                Destroy(_buttonA);

            // if the dialogue zone has no box collider, we do nothing and exit
            if (_collider==null)
                return; 

            // if the zone has already been activated and can't be activated more than once.
            if (_activated && !ActivableMoreThanOnce)
                return;

            // if the zone is not activable, we do nothing and exit
            if (!_activable)
                return;

            // if the player can't move while talking, we notify the game manager
            if (!CanMoveWhileTalking)
            {
                GameManager.Instance.FreezeCharacter();
            }

            // if it's not already playing, we'll initialize the dialogue box
            if (!_playing)
            {   
                createDialogeBox(lines[0].character);
                // the dialogue is now playing
                _playing=true;
            }
            // we start the next dialogue
            StartCoroutine(PlayNextDialogue());
        }

        protected void createDialogeBox(CorgiController character)
        {  
            // Remove the DialogeBox if exists  
            if (_dialogueBox)
                Destroy(_dialogueBox.gameObject); 
            
            Transform go = character.gameObject.GetComponent<Transform>();
            // we instantiate the dialogue box
            GameObject dialogueObject = (GameObject)Instantiate(Resources.Load("GUI/DialogueBox"));
            _dialogueBox = dialogueObject.GetComponent<DialogueBox>();
            // add it to owner, to follow it around
            _dialogueBox.transform.SetParent(character.transform);
            // we set its position
            _dialogueBox.transform.position=new Vector2(go.position.x,go.position.y+dialogue_offset.y); 
            // we set the color's and background's colors
            _dialogueBox.ChangeColor(TextBackgroundColor,TextColor);
            // if it's a button handled dialogue, we turn the A prompt on
            _dialogueBox.ButtonActive(ButtonHandled);
            // if we don't want to show the arrow, we tell that to the dialogue box
            if (!ArrowVisible)
                _dialogueBox.HideArrow();           

        }

        /// <summary>
        /// Plays the next dialogue in the queue
        /// </summary>
        protected override IEnumerator PlayNextDialogue()
        {       
            // we check that the dialogue box still exists
            if (_dialogueBox == null) 
            {
                yield return null;
            }
            // if this is not the first message
            if (_currentIndex!=0)
            {
                // we turn the message off
                _dialogueBox.FadeOut(FadeDuration); 
                // we wait for the specified transition time before playing the next dialogue
                yield return new WaitForSeconds(TransitionTime);
            }   

            // if we've reached the last dialogue line, we exit
            if (_currentIndex>=lines.Length)
            {
                _currentIndex=0;
                Destroy(_dialogueBox.gameObject);
                _collider.enabled=false;
                // we set activated to true as the dialogue zone has now been turned on     
                _activated=true;
                // we let the player move again
                if (!CanMoveWhileTalking)
                {
                    GameManager.Instance.UnFreezeCharacter();
                }
                if ((_character!=null))
                {               
                    _character.BehaviorState.InButtonActivatedZone=false;
                    _character.BehaviorState.ButtonActivatedZone=null;
                }
                // we turn the zone inactive for a while
                if (ActivableMoreThanOnce)
                {
                    _activable=false;
                    _playing=false;
                    StartCoroutine(Reactivate());
                }
                else
                {
                    gameObject.SetActive(false);
                }
                yield break;
            }

            // we check that the dialogue box still exists
            if (_dialogueBox.DialogueText!=null)
            {
                if (_previusCharacter != lines[_currentIndex].character)
                {
                    Destroy(_dialogueBox);
                    createDialogeBox(lines[_currentIndex].character);
                }
                //
                _previusCharacter = lines[_currentIndex].character;

                // every dialogue box starts with it fading in
                _dialogueBox.FadeIn(FadeDuration);
                // then we set the box's text with the current dialogue
                _dialogueBox.DialogueText.text=lines[_currentIndex].text;
            }

            _currentIndex++;

            // if the zone is not button handled, we start a coroutine to autoplay the next dialogue
            if (!ButtonHandled)
            {
                StartCoroutine(AutoNextDialogue());
            }
        }
    }
}