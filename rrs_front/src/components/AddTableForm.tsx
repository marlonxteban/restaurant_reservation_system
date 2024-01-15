import React, { useState, ChangeEvent, FormEvent } from "react";
import useSubmitTable, { TableFormatData } from "@/hooks/useSubmitTable";
import SuccessModal from "./SuccessModal";

// define component
const AddTableForm = () => {
    // define states
    const [tableData, setTableData] = useState<TableFormatData>({ capacity: 0, location: "" });
    const { isLoading, error, submitTable } = useSubmitTable();
    const [showSuccessModal, setShowSuccessModal] = useState<boolean>(false);

    // handle input change
    const handleChange = (event: ChangeEvent<HTMLInputElement>) => {
        const { name, value } = event.target;
        setTableData({
            ...tableData,
            [name]: name === "capacity" ? parseInt(value) : value,
        });
    };

    // Handle form submit
    const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        try {
            // Send data to API
            await submitTable(tableData);
            setShowSuccessModal(true);
        } catch (error) {
            console.log("Error at table creation", error);
        }
    };

    const handleCloseModal = () => {
        setShowSuccessModal(false);
        setTableData({ capacity: 0, location: "" });
    };

    return (
        <>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="capacity">Capacity:</label>
                    <input
                        type="number"
                        id="capacity"
                        name="capacity"
                        value={tableData.capacity}
                        onChange={handleChange}
                    />
                </div>
                <div>
                    <label htmlFor="location">Location:</label>
                    <input
                        type="text"
                        id="location"
                        name="location"
                        value={tableData.location}
                        onChange={handleChange}
                    />
                </div>
                <button type="submit">Add Table</button>
                {isLoading && <p>Loading...</p>}
                {error && <p>Error: {error.message}</p>}
            </form>
            <SuccessModal
                show={showSuccessModal}
                onHide={handleCloseModal}
                message="Table added successfully!"
            />
        </>
    );
};

export default AddTableForm;