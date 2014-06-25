package ch.bbv.meteorit.bean;

import ch.bbv.meteorit.persistence.db.Measurement;

public interface Persistence {

	public abstract void updateMeasurement(DataPoint value);

	public abstract Measurement getMeasurement(int sensorId, long timestamp);

	public abstract Measurement getMeasurement(int sensorId);

}