using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]

public class ScoreScript : MonoBehaviour
{
    private int myTeam;

    private Coroutine DisableRingCoroutine;

    public void setTeamOwner(int team)
    {
        myTeam = team;
    }

    private void OnTriggerEnter(Collider other)
    {
        // La quaffle pasa por el aro
        if(other.tag.Equals("Ball Quaffle"))
        {
            // El equipo contrario recibe puntos
            if(myTeam == 1)
            {
                GameManager.instancia.Score(2, 10);
            }
            else if(myTeam == 2)
            {
                GameManager.instancia.Score(1, 10);
            }
            Instantiate(GameManager.instancia.ScoreFX, transform.position, Quaternion.identity);
            GameManager.instancia.ScoreAudioSource.Play();

            // Deshabilitamos el collider durante un tiempo
            // para que no haya doble anotación
            if(DisableRingCoroutine!=null)
                StopCoroutine(DisableRingCoroutine);
            DisableRingCoroutine = StartCoroutine("DisableRing");
        }

        
    }

    private IEnumerator DisableRing()
    {
        GetComponent<MeshCollider>().enabled = false;

        yield return new WaitForSeconds(5f);

        GetComponent<MeshCollider>().enabled = true;
    }
}
