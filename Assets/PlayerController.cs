using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    #region references
    Rigidbody2D rb;
    [SerializeField] GameObject gunpoint;
    [SerializeField] GameObject projectile;
    #endregion references

    #region private
    [SerializeField] bool _fireOnCooldown = false;
    [SerializeField] float _fireCooldown = 0.3f;
    [SerializeField] float _speed = 2.5f;
    private float _fireExecutionTime;
    #endregion private

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetKey(KeyCode.W)) {
            rb.MoveRotation(Quaternion.identity);
            rb.velocity = _speed * Vector2.up;
        }
        else if (Input.GetKey(KeyCode.A)) {
            rb.MoveRotation(Quaternion.Euler(0f, 0f, 90f));
            rb.velocity = _speed * Vector2.left;
        }
        else if (Input.GetKey(KeyCode.S)) {
            rb.MoveRotation(Quaternion.Euler(0f, 0f, 180f));
            rb.velocity = _speed * Vector2.down;
        }
        else if (Input.GetKey(KeyCode.D)) {
            rb.MoveRotation(Quaternion.Euler(0f, 0f, 270f));
            rb.velocity = _speed * Vector2.right;
        }
        else {
            rb.velocity = Vector2.zero;
        }

        _fireOnCooldown = Time.time - _fireExecutionTime <= _fireCooldown;

        if (Input.GetKey(KeyCode.Space)) {
            Debug.Log(_fireCooldown);
            if (_fireOnCooldown) {
                return;
            }

            _fireOnCooldown = true;
            _fireExecutionTime = Time.time;

            var bullet = Instantiate(projectile, gunpoint.transform.position, transform.rotation);
            var bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.AddRelativeForce(Vector2.up * 100f);
            
            Destroy(bullet, 5f);
        }
                            

    }

    void FixedUpdate() {
    }

    #region private

    #endregion private
}
