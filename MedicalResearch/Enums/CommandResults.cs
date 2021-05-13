namespace MedicalResearch.Enums
{
	public enum CommandResults : int
	{
		ok = 0,
		db_error = 1,
		user_not_found = 2,
		user_removed = 3,
		wrong_email_or_password = 4,
		wrong_password = 5,
		not_found = 10,
		wrong_format_of_number = 11,
		wrong_clinic_id = 14,
		wrong_patient_id = 15,
		medicine_id_is_not_valid = 16,
		under_18_yo = 18,
		medicine_for_patient_not_found = 20
	}
}
