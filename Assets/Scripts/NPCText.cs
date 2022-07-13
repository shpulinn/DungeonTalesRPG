using UnityEngine;

public class NPCText : Collidable
{
    [SerializeField] private string message;
    [SerializeField] private float coolDownTime = 4.0f;

    private float _lastTimeShow;

    protected override void Start()
    {
        base.Start();
        _lastTimeShow = -coolDownTime;
    }

    protected override void OnCollide(Collider2D col)
    {
        if (Time.time - _lastTimeShow > coolDownTime)
        {
            GameManager.instance.ShowText(message, 25, Color.white, transform.position + new Vector3(0, 0.2f,0), Vector3.zero, coolDownTime);
            _lastTimeShow = Time.time;
        }
    }
}
