using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    public GameObject prefab;
    
    public float radius;
    
    public int number;



    [Range(1f,20f)]
    public float maxVelocity;

    [Range(0.1f, 20f)]
    public float radiusCohesion;

    [Range(0.1f, 20f)]
    public float radiusAlignment;

    [Range(0.1f, 20f)]
    public float radiusRepulsion;

    [Range(0.1f, 20f)]
    public float repulsionForce;

    [Range(1f, 20f)]
    public float radiusContainer;

    [Range(0.1f, 20f)]
    public float containerForce;

    [Range(0.1f, 20f)]
    public float followForce;



    Plant plantFollow = null;

    float radiusFollow = 4;

    bool isFollowing = false;



    GameObject[] boids;



    void Start()
    {
        boids = new GameObject[number];

        for(int i = 0; i < number; i++)
        {
            Vector3 position = this.transform.position + Random.insideUnitSphere * radius;
            position.y = 0;
            GameObject go = Instantiate(prefab, position, Random.rotation);
            go.transform.parent = this.transform;
            go.transform.localScale = Vector3.one * 0.15f;
            boids[i] = go;
        }
    }

    void Update()
    {
        for (int i = 0; i < number; i++)
        {
            State state = isFollowing ? State.Follow : State.Unfollow;
            if(state == State.Follow)
            {
                boids[i].GetComponent<Follow>().setCenter(plantFollow.getCenter());
                bool res = boids[i].GetComponent<Follow>().PointInSphere(boids[i].transform.position);
                if (res)
                {
                    state = State.Near;
                    plantFollow.decRadius();
                }
                if (plantFollow.shouldBeReset())
                {
                    plantFollow = null;
                    state = State.Unfollow;
                    isFollowing = false;
                }
            }
            stateMachine(boids[i], state);
        }
    }

    public void stateMachine(GameObject go, State state)
    {
        go.GetComponent<Alignment>().setRadius(radiusAlignment);
        go.GetComponent<Container>().setRadius(radiusContainer);
        go.GetComponent<Container>().setBoundaryForce(containerForce);
        go.GetComponent<Container>().setCenter(this.transform.position);

        switch (state)
        {
            case State.Unfollow:
                go.GetComponent<Boid>().setMaxVelocity(5);
                go.GetComponent<Cohesion>().setRadius(1.5f);
                go.GetComponentInChildren<Laser>().setPlant(null);
                go.GetComponent<InverseMagnetism>().setRadius(0.6f);
                go.GetComponent<InverseMagnetism>().setRepulsionForce(5);
                go.GetComponent<Follow>().setIsFollowing(false);
                go.GetComponent<Follow>().setFollowForce(0);
                break;
            
            case State.Follow:
                go.GetComponent<Boid>().setMaxVelocity(8);
                go.GetComponent<Cohesion>().setRadius(0.1f);
                go.GetComponentInChildren<Laser>().setPlant(null);
                go.GetComponent<InverseMagnetism>().setRadius(0.4f);
                go.GetComponent<InverseMagnetism>().setRepulsionForce(10);
                go.GetComponent<Follow>().setRadius(radiusFollow);
                go.GetComponent<Follow>().setCenter(plantFollow.getCenter());
                go.GetComponent<Follow>().setFollowForce(20);
                go.GetComponent<Follow>().setIsFollowing(true);
                break;
            
            case State.Near:
                go.GetComponent<Boid>().setMaxVelocity(5);
                go.GetComponentInChildren<Laser>().setPlant(plantFollow);
                go.GetComponent<Cohesion>().setRadius(2f);
                go.GetComponent<InverseMagnetism>().setRadius(0.3f);
                go.GetComponent<InverseMagnetism>().setRepulsionForce(8);
                go.GetComponent<Follow>().setRadius(radiusFollow);
                go.GetComponent<Follow>().setCenter(plantFollow.getCenter());
                go.GetComponent<Follow>().setFollowForce(15);
                go.GetComponent<Follow>().setIsFollowing(true);
                break;

            default:
                break;
        }
    }


    public enum State { Unfollow, Follow, Near }

    public void setIsFollowing(bool boolean)
    {
        this.isFollowing = boolean;
    }

    public void setPlantFollow(Plant p)
    {
        plantFollow = p;
    }
}
