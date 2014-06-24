package ch.bbv.meteorit.entities;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;

import ch.bbv.meteorit.bean.DataPoint;

@XmlRootElement
public class Measurement {

	@XmlElement(name = "Id")
	private Integer id;
	@XmlElement(name = "CityName")
	private String cityName;
	@XmlElement(name = "Timestamp")
	private Long timestamp;
	@XmlElement(name = "Temperature")
	private Double temperature;
	@XmlElement(name = "Pressure")
	private Double pressure;
	@XmlElement(name = "Humidity")
	private Integer humidity;

	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	public String getCityName() {
		return cityName;
	}

	public void setCityName(String cityName) {
		this.cityName = cityName;
	}

	public Long getTimestamp() {
		return timestamp;
	}

	public void setTimestamp(Long timestamp) {
		this.timestamp = timestamp;
	}

	public Double getTemperature() {
		return temperature;
	}

	public void setTemperature(Double temperature) {
		this.temperature = temperature;
	}

	public Double getPressure() {
		return pressure;
	}

	public void setPressure(Double pressure) {
		this.pressure = pressure;
	}

	public Integer getHumidity() {
		return humidity;
	}

	public void setHumidity(Integer humidity) {
		this.humidity = humidity;
	}

	public void updateMeasurements(DataPoint measurement, long timestamp) {
		this.timestamp = timestamp;
		this.id = measurement.getId();
		this.cityName = "Davos";
		if ("Pressure".equals(measurement.getType())) {
			this.pressure = measurement.getValue();
		} else if ("Temperature".equals(measurement.getType())) {
			this.temperature = measurement.getValue();
		} else if ("Humidity".equals(measurement.getType())) {
			this.humidity = measurement.getValue().intValue();
		}
	}

}
