using Code.Extensions;
using UnityEngine;

namespace Code.Vehicle
{
    public class WheelView: MonoBehaviour
    {
        [SerializeField, ShowOnly] private WheelCollider _wheel;
        [SerializeField] private EWheelSide _side;
        
        [SerializeField] private bool _isSteering;
        [SerializeField] private bool _isMotor;


        public WheelCollider Wheel => _wheel;
        public EWheelSide Side => _side;
        
        public bool IsSteering => _isSteering;
        public bool IsMotor => _isMotor;


        private void Start()
        {
            _wheel = GetComponentInChildren<WheelCollider>();
        }
    }
}