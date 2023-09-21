using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{
    private readonly NetworkVariable<PlayerNetWorkData> _netState = new(writePerm: NetworkVariableWritePermission.Owner);
    private Vector3 vel;
    private float rotVel;
    [SerializeField] private float _cheapInterpolationTime = 0.1f;
    // Update is called once per frame
    void Update()
    {
        if(IsOwner)
        {
            _netState.Value = new PlayerNetWorkData()
            {
                Position = transform.position,
                Rotation = transform.rotation.eulerAngles
            };
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, _netState.Value.Position, ref vel, _cheapInterpolationTime);
            transform.rotation = Quaternion.Euler(0, Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y, _netState.Value.Rotation.y, ref rotVel, _cheapInterpolationTime), 0);
        }
    }
    struct PlayerNetWorkData : INetworkSerializable
    {
        private float x, z;
        private float yRot;

        internal Vector3 Position
        {
            get => new Vector3(x, 1, z);
            set
            {
                x = value.x;
                z = value.z;
            }
        }

        internal Vector3 Rotation
        {
            get => new Vector3(0 ,yRot, 0);
            set
            {
                yRot = value.y;
            }
        }
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref x);
            serializer.SerializeValue(ref z);
            serializer.SerializeValue(ref yRot);
        }
    }
}
