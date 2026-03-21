using UnityEngine;

public class DataManeger : MonoBehaviour
{
    public static DataManeger Instance;
    [Header("Const")]

    public float Scale = 0.5f;

    [Header("Data")]
    public float Latitude = 0;
    public float Longitude = 0;

    public float Tempature = 0;
    public float Pressure = 0;
    public float Humidity = 0;
    public float Altitude = 0;

    public float t = 0;
    public float DX = 0;
    public float DY = 0;

    public float TVOC = 0;
    public float eCO2 = 0;



    private void Awake()
    {
        DataManeger.Instance = this;
    }

    private void Start()
    {
        Rocket.instance.SetTrailState(false);
    }


    public static void UpdateData(string Data)
    {
        string[] RealData = Data.Split(',');

        Vector3 velocity, rotation, acceleration;

        velocity = Vector3.zero; rotation = Vector3.zero; acceleration = Vector3.zero;

        

        foreach (string d in RealData)
        {
            string data = d.Substring(2);
            if (d.StartsWith("et")) {
                DataManeger.Instance.t = float.Parse(data) * 0.001f;
            }
            if (d.StartsWith("rx"))
            {
               rotation.x = float.Parse(data) * Mathf.Rad2Deg;
            }
            if (d.StartsWith("ry"))
            {
                rotation.y = float.Parse(data) * Mathf.Rad2Deg;
            }
            if (d.StartsWith("rz"))
            {
                rotation.z = float.Parse(data) * Mathf.Rad2Deg;
            }
            if (d.StartsWith("ax"))
            {
                acceleration.x = float.Parse(data);
            }
            if (d.StartsWith("ay"))
            {
                acceleration.y = float.Parse(data);  
            }
            if (d.StartsWith("az"))
            {
                acceleration.z = float.Parse(data);
            }
            if (d.StartsWith("vx")) velocity.x = float.Parse(data);
            if (d.StartsWith("vy")) velocity.y = float.Parse(data);
            if (d.StartsWith("vz")) velocity.z = float.Parse(data);

            if (d.StartsWith("dx")) DataManeger.Instance.DX = float.Parse(data);
            if (d.StartsWith("dy")) DataManeger.Instance.DY = float.Parse(data);
            if (d.StartsWith("tp")) DataManeger.Instance.Tempature = float.Parse(data);
            if (d.StartsWith("pr")) DataManeger.Instance.Pressure = float.Parse(data);
            if (d.StartsWith("hu")) DataManeger.Instance.Humidity = float.Parse(data);
            if (d.StartsWith("la")) DataManeger.Instance.Latitude = float.Parse(data);
            if (d.StartsWith("lo")) DataManeger.Instance.Longitude = float.Parse(data);
            if (d.StartsWith("al")) DataManeger.Instance.Altitude = float.Parse(data);


            if (d.StartsWith("tv")) DataManeger.Instance.TVOC = float.Parse(data);
            if (d.StartsWith("ec")) DataManeger.Instance.eCO2 = float.Parse(data);

        }

        UIManeger UIM = UIManeger.Instance;
        DataManeger DM = Instance;



        Rocket.instance.setTargetRotation(rotation);

        UIM.UpdateAccel(acceleration);    
        Rocket.instance.SetTrailState((acceleration.z > 20));
        UIM.UpdateTeapotDisplay(rotation);
        
        UIM.UpdateAltitude(DM.Altitude);
        UIM.UpdateElapasedTime(DM.t);
        
        UIM.UpdateGPS(DM.Latitude, DM.Longitude);

        UIM.UpdateVel(velocity);

         
        UIM.UpdateHumidity(DM.Humidity);
        UIM.UpdateTemp(DM.Tempature);
        UIM.UpdatePressure(DM.Pressure);
        UIM.UpdateDXDY(DM.DX, DM.DY);

        UIM.setSGPData(DM.eCO2, DM.TVOC);

        Rocket.instance.TargetPosition = new Vector3(DM.DX * DM.Scale, DM.Altitude * DM.Scale, DM.DY * DM.Scale);

    }
}
