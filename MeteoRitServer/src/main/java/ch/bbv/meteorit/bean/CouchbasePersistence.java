package ch.bbv.meteorit.bean;

import java.io.IOException;
import java.net.URI;
import java.net.URISyntaxException;
import java.util.Arrays;
import java.util.List;

import javax.inject.Singleton;

import ch.bbv.meteorit.entities.Measurement;

import com.couchbase.client.CouchbaseClient;

@Singleton
public class CouchbasePersistence implements Persistence {

	CouchbaseClient client;
	
	public CouchbasePersistence() throws URISyntaxException, IOException {
		List<URI> hosts = Arrays.asList(new URI("http://192.168.1.141:8091/pools"));
		String bucket = "default";
		String password = "";
		client = new CouchbaseClient(hosts, bucket, password);
	}

	@Override
	public void updateMeasurement(DataPoint value) {
		
	}

	@Override
	public Measurement getMeasurement(int sensorId, long timestamp) {
		return null;
	}

	@Override
	public Measurement getMeasurement(int sensorId) {
		return null;
	}
	
	
}
