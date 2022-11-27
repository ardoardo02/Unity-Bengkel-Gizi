using System.Collections;

using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public enum SpawnState
    {
        Spawning,
        Waiting,
        Counting
    }

    [System.Serializable]
    public class Customer
    {
        public string name;
        public Transform Cust;
    }
    public Customer[] customers;
    private int nextWave = 0;

    [Header("Time Between Customer")]
    [SerializeField] private float minTimeBC= 3;
    [SerializeField] private float maxTimeBC= 6;
    private float timeBetweenCust;

    [Header("Time Started Customer")]
    public float timeStartedCust;

    [Header("Customer Countdown")]
    public float CustPatience;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.Counting;

    private void Start()
    {
        CustPatience = timeStartedCust;
    }

    private void Update()
    {
        if (state == SpawnState.Waiting)
        {
            Debug.Log(state);
            if (CustWaiting() == false)
            {
                CustServed();
            }
            else
            {
                return;
            }
        }

        if (GameManager.Instance.CheckCustDone())
        {
            return;
        }

        if (CustPatience <= 0 )
        {
            if(state != SpawnState.Spawning)
            {
                SpawnCust(customers[nextWave]);
            }
        }
        else
        {
            CustPatience -= Time.deltaTime;
        }
    }

    void CustServed()
    {
        state = SpawnState.Counting;
        timeBetweenCust = Random.Range(minTimeBC, maxTimeBC);
        CustPatience = timeBetweenCust;
        if(nextWave + 1 > customers.Length - 1)
        {
            //GameManager.Instance.CustServe();
            Debug.Log("All Done");
        }
        else
        {
            nextWave++;
        }
    }

    // boolean kesabaran customer belum habis
    bool CustWaiting()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown  <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Customer") == null)
            {
                return false;
            }
        }
        return true;
    }

    public void SpawnCust(Customer _wave)
    {
        GameManager.Instance.CustSpawn();
        state = SpawnState.Spawning;
        InstantiateCust(_wave.Cust);
        state = SpawnState.Waiting;
    }

    void InstantiateCust(Transform _Cust)
    {
        Debug.Log("Instantitate Customer");
        Instantiate(_Cust, transform.position, transform.rotation);
    }
}
