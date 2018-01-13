'use strict';

import React from 'react';
import {
  AppRegistry,
  StyleSheet,
  Text,
  View,
  ScrollView
} from 'react-native';

class HelloWorld extends React.Component {
  render() {
    return (
      <View>
       <View style={{width: 300, height: 300, backgroundColor:'rgba(0,52,52,0.4)'}}>
	      	<ScrollView>
	          <Text style={{fontSize:96}}>React scroll</Text>
	          <Text style={{fontSize:96}}>React scroll</Text>
	          <Text style={{fontSize:96}}>React scroll</Text>
	        </ScrollView>
        </View>
      </View>
    );
  }
}
var styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    backgroundColor: 'transparent',
  },
  hello: {
    fontSize: 20,
    textAlign: 'center',
    margin: 10,
  },
});

AppRegistry.registerComponent('UnityReact', () => HelloWorld);