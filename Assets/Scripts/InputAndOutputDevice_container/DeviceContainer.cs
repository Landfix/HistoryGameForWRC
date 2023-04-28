using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InputAndOutputDevice_container
{
    public class DeviceContainer : MonoBehaviour
    {
        [SerializeField] private List<Device> _devices;
        [SerializeField] private List<FakeDevice> _fakeDevices;


        private void Start()
        {
            foreach (Device device in _devices)
            {
                device.Initialize();
                device.Selected += DeviceSelected;
            }

            foreach (FakeDevice fakeDevice in _fakeDevices)
            {
                fakeDevice.Initialize();
                fakeDevice.Unselected += FakeDeviceUnselected;
            }
        }

        private void FakeDeviceUnselected()
        {
            if (_devices.All(x => x.IsSelected) 
                && _fakeDevices.All(x => !x.IsSelected))
            {
                StartCoroutine(AllSelectedDevicesCoroutine());
            }
        }

        private void DeviceSelected()
        {
            if (_devices.All(x => x.IsSelected) 
                && _fakeDevices.All(x => !x.IsSelected))
            {
                StartCoroutine(AllSelectedDevicesCoroutine());
            }
        }
        
        private IEnumerator AllSelectedDevicesCoroutine()
        {
            // todo recast
            yield return new WaitForSeconds(0.7f);
            for (int i = 0; i < _devices.Count; i++) 
                _devices[i].Selected -= DeviceSelected;
            
            StartNewScene();
        }

        private static void StartNewScene()
        {
            if (SceneManager.sceneCountInBuildSettings -1 > SceneManager.GetActiveScene().buildIndex)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                SceneManager.LoadScene(0);
        }
    }
}