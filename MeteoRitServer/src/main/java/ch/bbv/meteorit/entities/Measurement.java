package ch.bbv.meteorit.entities;

import java.io.Serializable;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;

import ch.bbv.meteorit.bean.DataPoint;

@XmlRootElement
public class Measurement implements Serializable {

	private static final long serialVersionUID = 4835981952244463678L;
	
	@XmlElement(name = "Id")
	private int id;
	@XmlElement(name = "CityName")
	private String cityName;
	@XmlElement(name = "Timestamp")
	private long timestamp;
	@XmlElement(name = "Temperature")
	private double temperature;
	@XmlElement(name = "Pressure")
	private double pressure;
	@XmlElement(name = "Humidity")
	private int humidity;

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public String getCityName() {
		return cityName;
	}

	public void setCityName(String cityName) {
		this.cityName = cityName;
	}

	public long getTimestamp() {
		return timestamp;
	}

	public void setTimestamp(long timestamp) {
		this.timestamp = timestamp;
	}

	public double getTemperature() {
		return temperature;
	}

	public void setTemperature(double temperature) {
		this.temperature = temperature;
	}

	public double getPressure() {
		return pressure;
	}

	public void setPressure(double pressure) {
		this.pressure = pressure;
	}

	public int getHumidity() {
		return humidity;
	}

	public void setHumidity(int humidity) {
		this.humidity = humidity;
	}

	public void updateMeasurements(DataPoint value, long timestamp) {
		this.timestamp = timestamp;
		this.id = value.getId();
		if ("Pressure".equals(value.getType())) {
			this.pressure = value.getValue();
		} else if ("Temperature".equals(value.getType())) {
			this.temperature = value.getValue();
		} else if ("Humidity".equals(value.getType())) {
			this.humidity = value.getValue().intValue();
		}
	}

}
