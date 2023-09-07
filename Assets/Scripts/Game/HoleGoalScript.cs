//Hole Goal Script
//by: Jackson
//Last Edited: 6/9/2023 3:18 pm

using UnityEngine;

/// <summary>
/// A simple scripte applied to the goal object prefab.
/// Only function is to run code when the ball enters the goal.
/// - Jackson
/// </summary>
public class HoleGoalScript : MonoBehaviour
{
    ParticleSystem m_successParticles;
    [Tooltip("An indecator of when the goal is reached.")]
    public bool m_GoalReached = false;
    private void Start()
    {
        m_successParticles = GetComponentInChildren<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //TODO: actually do something when the goal is reached
            Debug.Log("The player has reached the goal!");
            m_GoalReached = true;
            //This can be removed if we still want the ball post goal
            Destroy(other.transform.parent.gameObject);

            //Particles to clearly indecate to the player that the ball has reached the goal
            m_successParticles.Play();

        }
    }
}
